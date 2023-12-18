I have used:
- Visual Studio 2022 Professional
- .NET Framework 4.6.1
- SQL Server Management Studio 19

Libs I have installed:
- Caliburn.Micro 3.2.0
- Caliburn.Micro.Core 3.2.0
- Microsoft.EntityFrameworkCore.SqlServer 3.1.32
- Microsoft.EntityFrameworkCore.Tools 3.1.32

Libs I have added:
- System.Configuration.dll

Scaffolded the db with the command:
- scaffold-dbcontext "Data Source=LAPTOP-77MC98HD;Initial Catalog=Caliburn2DB;Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer

Database:
- Set the database connection string in the App.config connectionStrings section, to the value of the LinkTekTest tag.

The colors (style) used in the application can be changed by modifying the following values in the Styles.xaml:
    <SolidColorBrush x:Key="BackgroundColorLight" Color="#33CCCC" />
    <SolidColorBrush x:Key="BackgroundColorMedium" Color="#05a7b4" />
    <SolidColorBrush x:Key="BackgroundColorDark" Color="#0f849d" />
    <SolidColorBrush x:Key="ForegroundColorEnabled" Color="White" />
    <SolidColorBrush x:Key="ForegroundColorDisabled" Color="DarkGray" />
    <SolidColorBrush x:Key="AlternatingRowBackgroundColor" Color="#00b9bf" />
    <SolidColorBrush x:Key="HoverBackgroundColor" Color="LightBlue" />

MainView:
- The data is loaded by default on start up, there is no need for the "Load" button to be used.
- The "Load" button may be used only if the database is updated outside this application.
- The data is paginated and can be sorted ascendent and descendent. The page size is defined in the Paginator class.

EditView
- The field "Address 2" can be set to empty text by the user, the "Address 1" cannot (forbidden through rule validation).
- When the data of a field is incorrect the text block border is highlighted with red and the error is shown in the tooltip of the text block. The Ok button is disabled.
- The items in the "States" combo box is the list of distinct values asynchronously retrieved from the database's State column in the Customer table at the first opening of the Edit view. After that the list is kept in memory.
- The field CreatedDate cannot be modified by the user.
- The field UpdatedDate of a costumer is set to DateTime.Now any time a customer is updated.
- When the Customer is saved, the EditView is closed and the MainView is automatically updated with the modifications.

I'd improve the project with the following:
- Add the ability to select the index of page the user want to jump to - currently there are only the "First Page", "Previous Page", "Next Page" and "Last Page" page buttons.
- Add the ability to select the size of the page (number of items shown at once in the grid).
- Retrieve the list of the available states from another database table (from a specific States table) or from a file.
- Style the State ComboBox.
- Use a better way for displaying the errors that may appear in the application - currently simple MessageBoxes are used.
- Enable the "Ok" button in the Edit view only when the data of the customer was modified - at the moment the button is Enabled at the moment the view is opened.

I really enjoyed implementing it, I'd happily work further on it!