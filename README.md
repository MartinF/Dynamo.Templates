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
In this example a template is declared in a razor view using a helper.

The template is loaded using the @Template.Get(() => MessageTemplate()) method call which outputs the compiled template to the view.

The compiled template will be available as a JavaScript method with the same name (MessageTemplate - there are several overloads of the Get method if you wish to use a different name), that can be used by passing in the model data as a parameter (var result = MessageTemplate(model);).
``` HTML
@helper MessageTemplate()
{
	<ul>
		{{ for(var i = 0; i < model.messages.length; i++) { }}
		<li>{{: model.messages[i].username }}: {{: model.messages[i].message }}</li>
		{{ } }}		
	</ul>
}

<div id="example"></div>

<script type="text/javascript">
	// Outputs the Template
	@Template.Get(() => MessageTemplate())
	
	// Model with data for the template
	var model = { messages:[{username: "User1", message: "Hello"}, {username: "User2", message: "World!"}]};
	
	// Get the template result
	var result = MessageTemplate(model);
	
	// Insert the result
	document.getElementById("example").innerHTML = result;
</script>
```

## Help
If you find this project useful and want to help with improving it, you are more than welcome.
Please report any errors you find.

## To-do
- Finish Console project
- Create Visual Studio add-in that makes it easy to compile templates on save. 
- Add more tests.
