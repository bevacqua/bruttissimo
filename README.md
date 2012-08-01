Bruttissimo
===========


Conventions
===========


Script and Style Resources:
---------------------------

	- localization resources located in ~/Content/Script/Resources and ~/Common.prj/Resources/Shared/ are passed to the client through JavaScript after being minified.
	- all CSS is bundled and minified into a single stylesheet.
	- static JS is bundled and minified into a single script file. views are to contain the smallest amount of JS code possible.
	- view-specific code that is static should be invoked from the view, but be in the bundled script file.
	- sometimes razor parameters are to be passed to these static files, keep them out of the static files, and pass them as arguments when invoked from the view.
	- not all javascript resources are actually used, some are just for reference, like the un-minified version of jquery.


Exception Handling:
-------------------

	- throwing an ExpectedException in a controller will return a JsonResult specifying the message set in the ExpectedException. This produces a dialog box with the error message.
	- if the Request was non-AJAX, the UnhandledError view will be displayed instead, and display the message.
	- for exceptions that do not occur in the context of a controller (say a resource was not found), the global handler will take care of that.
	- all exceptions are logged through log4net into a database log table.
	- SQL exceptions are also logged through log4net.


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
	- SignalR for persistent client/server connections for realtime updates.

	- MVC arquitecture.
	- Custom "Mini" membership schema and providers, including Principal and Identity injection.
	- Inversion of Control, Dependency Injection via constructors and properties.
	- POCO Entities.
	- Dapper Micro-ORM, for performance boost and simplified database querying.
	- Log4Net logging, which tracks exceptions, stack traces, SQL code and request endpoints.
	- Mvc MiniProfiler implementation to rapidly identify performance bottlenecks.
	- Exception handling for both AJAX and non-AJAX requests.
	- Both server and client-side validation using FluentValidation.
	- DRY approach to string resource handling.
	- Unit Tests over the application logic.


TODO
====

- MINOR
	- profit! https://merchant.paypal.com/us/cgi-bin/?cmd=_render-content&content_ID=merchant/donations
	- robots.txt & sitemap.xml 
	- sprite con las imagenes del site.
	- design
	- browser cultures
	- NTH: http://samsaffron.com/archive/2012/06/07/testing-3-million-hyperlinks-lessons-learned (link rot handler).
	- NTH: http://samsaffron.com/archive/2011/10/13/optimising-asp-net-mvc3-routing (regex compilation warning).
	- mobile-aware email templates
	- NTH: nuget packages over bruttissimo.external?
	- NTH: figure out a way to do the home redirect without using ExtendedAuthorize (and using regular [Authorize])
----------

- FacebookClient client = new FacebookClient(accessToken); // TODO: move to FacebookProvider, along with an exception wrapper.

- POSTS
	-(index) browsersize-based ajax, if the browser window is too small, then we request less articles.
	
	-(index) look and feel when updating the ui with more posts:
		- scroll down a bit when it has results?

	- no hay más posts para mostrar si trae < del count pedido (si trae == no importa)
	- cargar og:data con el post más reciente de la página!
	- joins qué pasa si el user es null? el user es null? si traigo de fb lo debería crear, no?
	- qué pasa con los image links?
