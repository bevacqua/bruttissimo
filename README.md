Bruttissimo
===========

https://github.com/bevacqua/Bruttissimo/


Conventions
===========


Script and Style Resources:
---------------------------

	- localization resources located in specific folders are passed to the client as JavaScript after being minified.
	- all CSS is bundled and minified into a single stylesheet.
	- static JS is bundled and minified into a single script file. views are to contain the smallest amount of JS code possible.
	- view-specific code that is static should be invoked from the view, but be in the bundled script file.
	- sometimes razor parameters are to be passed to these static files, keep them out of the static files, and pass them as arguments when invoked from the view.
	- not all javascript resources are actually referenced in the website, some are just for intellisense or code lookups, like the un-minified version of jquery.


Exception Handling:
-------------------

	- if the Request was non-AJAX, the UnhandledError view will be displayed instead, and display the message.
	- for exceptions that do not occur in the context of a controller (say a resource was not found), the global handler will take care of that.
	- all exceptions are logged through log4net into a database log table.
	- SQL exceptions are also logged through log4net.
    - realtime application logs thanks to SignalR and log4net.


Features:
---------

	- HTML 5 semantic markup.
	- CSS 3 and graceful degration to earlier browsers.
	- CSS Bundling, and minification.
	- LESS stylesheets to make CSS more DRY.
	- responsively designed tooltips (that gracefully degrade).
	- Mobile First Responsive Web Design.
	- html5shiv for IE browsers lower than IE9, which don't support HTML5 tags.
	- JS Bundling and Minification.
	- Progressive enhancement and unobtrusive javascript.
	- Unobtrusive javascript further enhanced by using a convention where .js.cshtml files are the javascript counterpart to views (separation of concerns).
	- Unobtrusive AJAX automation converting action ViewResults into AjaxViewResults.
	- SignalR persistent client/server connections for realtime updates.

	- MVC architecture.
	- Custom "Mini" membership schema and providers, including Principal and Identity injection.
	- Inversion of Control, Dependency Injection via constructors and properties.
	- POCO Entities.
	- Dapper Micro-ORM, for performance boost and simplified database querying.
	- DbUp for automatic build-step database schema upgrading.
	- Log4Net logging, which tracks exceptions, stack traces, SQL code and request endpoints.
	- Mvc MiniProfiler implementation to rapidly identify performance bottlenecks.
    - Quartz.NET job scheduling for long running background tasks.
	- Both server and client-side validation using FluentValidation.
	- DRY approach to string resource handling.
	- Unit Tests over the application logic.
    - nuget packaging to reduce overall digital footprint of the solution.