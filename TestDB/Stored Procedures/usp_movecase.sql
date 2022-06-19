CREATE PROCEDURE [dbo].[usp_movecase] 
  @casetomove UNIQUEIDENTIFIER
WITH ENCRYPTION
AS
BEGIN
  SET NOCOUNT ON;
   
  DECLARE @Result AS INT = 0
  DECLARE @_errormessage NVARCHAR(MAX)  
  DECLARE @TimeStamp datetime;
  SET @TimeStamp = GETDATE();

  BEGIN TRANSACTION 
    BEGIN TRY

		-- get current pallet guid where given case resides
		DECLARE @CurrentPalletGuid VARCHAR(36);
		SET @CurrentPalletGuid = ( SELECT palletguid FROM [case]
									WHERE guid = @casetomove);
		PRINT '@CurrentPalletGuid - ' +  @CurrentPalletGuid;

		-- get current product guid that is inside case
		DECLARE @ProductGuid VARCHAR(36);
		SET @ProductGuid = (SELECT productguid 
								FROM [case] 
								WHERE guid = @casetomove);
		PRINT '@ProductGuid - ' + @ProductGuid;

		-- get product category
		DECLARE @CaseProductCategoryName VARCHAR(36);
		SET @CaseProductCategoryName = (SELECT pc.name
									FROM product p, productcategory pc
									WHERE p.productcategoryguid = pc.guid AND p.guid = @ProductGuid);
		PRINT '@CaseProductCategoryName - ' + @CaseProductCategoryName;

		-- set the destination palleguid
		DECLARE @DestinationPalletGuid VARCHAR(36);
		SET @DestinationPalletGuid = 
			(
                -- get a pallet id that can receive the case
				SELECT DISTINCT TOP(1) c.palletguid
				FROM [case] c
					INNER JOIN
					(
						-- get products with categories
						SELECT p.guid AS product_guid, p.productcategoryguid, pc.name AS category_name, p.name AS product_name, pc.name AS product_category
						FROM product p
						 INNER JOIN
						productcategory pc
						ON p.productcategoryguid = pc.guid
					) AS result ON c.productguid = result.product_guid
				WHERE category_name != @CaseProductCategoryName AND
					  palletguid != @CurrentPalletGuid
				--order by c.productguid;
			 );

		PRINT '@DestinationPalletGuid - ' + @DestinationPalletGuid;

		IF (@DestinationPalletGuid IS NULL)
			PRINT 'no suitable pallet was found!';
		ELSE
			UPDATE [case] 
			SET palletguid = @DestinationPalletGuid, modifieddate = @TimeStamp
			WHERE guid = @CaseToMove;
			PRINT 'successfully moved case to new pallet!';
    END TRY
    BEGIN CATCH
      SET @Result = -1    

      SELECT @_errormessage =  ERROR_MESSAGE()
            
    END CATCH

  IF (@@TRANCOUNT > 0)
  BEGIN
    IF (@Result >= 0)
    BEGIN      
      COMMIT TRANSACTION            
      PRINT 'Commited Transaction'
    END
    ELSE
    BEGIN      
      ROLLBACK TRANSACTION
      PRINT 'Rolled back Transaction because of error: ' + @_errormessage
    END
  END  
  
  RETURN @Result  
END