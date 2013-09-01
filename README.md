# Server-side Templates
Dynamo.Template is a server-side template engine/compiler.

A Template can consist of a mix of HTML and JavaScript. 

It is possible to compile templates inline or to an external file to make it reusable and easy to include multiple places.

All compiled templates are cached to ensure maximum output performance.

Benifits:
- Not yet another JS library to include (saves a request, KB across the wire and browser load time)
- Pre-compiled - Saves processing time

## Syntax
The syntax is very simple as it just HTML, mixed with 2 different types of script statements.
You can use {{ Block }} to define a script block, and {{: Print }} to define a print statement.
See the example below for how they are used.

## Examples
...

## Help
If you find this project useful and want to help with improving it, you are more than welcome.
Please report any errors you find.

## To-do
- Finish Console project
- Create Visual Studio add-in that makes it easy to compile templates on save. 
- Add more tests.
