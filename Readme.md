# CAT2 Assessment

## Backend: CAT2 Test API
Cat Squared is developing a new service for managing the weather forcast to aid in the scheduling of flocks from Farms in warm climates.

The attached example API is to provide the forecast for the specified date in the future, however we do not presently have any automated tests.

Our request is for the viewer to create a set of tests to provide solid coverage, and exercise the API's limits. Aim for 100%
code coverage (of stuff that makes sense to test). These can be unit tests and/or integration tests. To make things easier
to test, it might be necessary to add or refactor the existing code.

---
## Frontend: CAT2 Test UI
Cat Squared is developing a new Angular 13 application (Typescript) that consumes the CAT2 Test API to show the end user the current
forecast using Bootstrap. 

The following is a list of tasks to accomplish:

- Write Karma Unit Tests for any methods inside of app.component.ts that is already there or any additional code you add, making sure they pass.
- Format the weather datetime that is displayed in app.component.html
- Bonus points for formatting the weather description using the following colors
  - Cyan = Freezing, Bracing, Chilly
  - Green = Mild, Balmy, Cool
  - Orange = Warm, Hot
  - Red = Sweltering, Scorching

---
## Database: CAT2 Test DB
The attached example DB is a basic database relating cases, pallets, products, and product categories. We want to modify the usp_movecase stored procedure to move a case to a new pallet.
The pallet being moved to must meet these criteria      
  * The other cases on the pallet that is being moved to must be of the same product category as the case that was passed in   
    * Assume all cases on pallets are already of the same product category (or bonus points, don't)
  * The pallet being moved to must not be the pallet the case is currently on
  * (Bonus) Add any optimizations to the table structures and procedure that you identify

### Additional Information
You can publish the "TestDB" sqlproj to a SQL Server instance you have access to. This will create the DB strucure and populate it with seed data that can be used for testing.

To test the usp_movecase procedure on a published DB you can execute: 
    * EXEC dbo.usp_movecase '08bbb8f6-65b7-46a4-9da3-b8852b1d1fcb'

