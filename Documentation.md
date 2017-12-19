# Online Contracting Documentation
  1. ### Controllers
  2. ### Views
  3. ### PDFs
  4. ### CSS and JS

_________________

## 1. Controllers

### Account Controller  

  1. Built by Visual Studio, Below are the changes made


  2. `Task<ActionResult> Register(RegisterViewModel model)`
    * Here is where Users are set to inactive when registering (line 158)  
    * After that we send an email stating a user needs to be activated


### Admin Controller

  1. `void UpdateInfoTable(int id, string column, int value)`
    * Used in SuperAdmin View to save input values to database


  2. `string GetAdvisorStatus(int status)`
    * Used for daily email
    * Creates Html string that is formatted in body of email


  3. `ActionResult Edit(FormCollection userEdit)`
    * Saves changes to user accounts, mostly used to change roles
    * Emails user if they have become activated


  4. `string GetRole(string id)`
    * Helper function used to display role names in tables on edit view


### Agent Controller

  1. `ActionResult Generate(FormCollection formValues, string Id)`
    * Creates contracts using `Id`
    * Populates contracts using `formValues`, from the `contractForm` view

### Home Controller

  1. `ActionResult RedirectToHomePage()`
    * This is called from index method so that the `www.url.com` link is what redirects
    * Grabs the user, finds that users role and redirects based on that role


  2. `void DailyEmail()`
    * Creates current database object
    * Compares that to old database object
    * If values are different, it inserts the entry into a formatted string
    * Calls send email to send a string of changes to specified email


  3. `void SendEmail(string message, string reciepient, string subject)`
    * Builds and sends email object based on parameters passed
    * Added email template that may be replaced by signiture


  4. `string GetStatus(int? InfoStatus)`
    * Helper function for `DailyEmail`, turns our interger system into logical string for UI


  5. `static class Infoes`
    * Had to build class to store database variables, acting as Global variable
    * May be a work around, if better way please change
-------------
## 2. Views
### Account Views
  1. Only changed `login.cshtml`to remove features built by VS that we were not using


### Admin Views
  1. `_AgentTable.cshtml`
    * Used to display the given excel document format.
    * Redered Partial View for users of role `Admin`


  2. `_AgentTableEditable.cshtml`
    * Same layout as `_AgentTable.cshtml`
    * Edit/save functions on data
    * Redered Partial View for users of role `SuperAdmin`


  3. `Delete.cshtml`
    * Built by VS, used to delete users


  4. `Edit.cshtml`
    * Built by VS, removed some fields as the only change.
    * Used to change roles of users mostly


  5. `Index.cshtml`
    * Main view for `Admin` or `SuperAdmin` roles
    * We have navigation buttons at top
    * Select list populated by all agents and pending/requested
    * The tables are all rendered upon loading the page, we just hide them until the user selects one table. This makes the page load slow, should be done by user selecting then loading tables.


  6. `SuperAdmin.cshtml`
    * Used to view users, only accessable by SuperAdmin role

### Agent Views
  1. `ContractForm.cshtml`
    * Form for user to enter contract information
    * Must have a field, with fieldname matching pdf fieldname, in this form to be entered into contract
    * Users are initially directed here until it is filled out


  2. `Index.cshtml`
    * Main view for users
    * Used to pull populated contracts from site
    * Cannot be accessed by users until contract form is filled out, admins can access at anytime.

### Home Views
  1. `Inactive.cshtml`
    * Presented to users that have completed registration but have not been given a role in the system
    * Users MUST sign out after being placed in a role to move past this screen

### Manage Views  
  1. All built by VS, not sure any are used

### Shared
  1. `_Layout.cshtml`
    * Built by VS, added SMS/Pinnicle Logos and other content changes


  2. `_LoginPartial`, `Error` and `Lockout`
    * All built by VS and no changes other than removing documentation/similar from view

_________________
## 3. PDFs
  1. 4 Basic Contracts: `AetnaContract`, `MedicoContract`, `MutualContract`, and `TransamericaPremierContract`
  2. Generated PDFs are saved with user id concatenated to the basic contract names

_________________
## 4. CSS and JS
  1. CSS
    * Located in site.css under content folder
    * `¯\_(ツ)_/¯`

  2. Javascript
    1. `changeData(info_id, carrier_name, column_name)`
      * Used by AgentTableEditable to check user entered date and save changes to the table

    2. `saveDate(id, date)` and `saveNote(id, note)`
      * Saves date/note entered on `_AgentTableEditable` to database

    3. `addStyle()`
      * Used on `_AgentTable` and `_AgentTableEditable` to give cells color

    4. `showAdvisorStates(selection)`
      * Used on `SuperAdmin` to only display one selection at a time
