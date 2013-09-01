# Server-side Templates
Dynamo.Template is a server-side template engine/compiler. 

A Template can consist of a mix of HTML and JavaScript. 

All compiled templates are cached to ensure maximum output performance.

Benifits:
- Not yet another JS library to include (saves a request, KB across the wire and browser load time)
- Pre-compiled - Saves processing time


ps. if you prefer to compile all your templates to a file that can be included multiple places it is of course possible.


## Syntax
The syntax is very simple as it just pure html, mixed with 2 different types of script statements.
You can use {{ Block }} to define a script block, and {{: Print }} to define a print statement.
See the example below for how they are used.

## Examples
...

## Help
If you find this project useful and want to help with improving it, you are more than welcome.
Please report any errors you find.
