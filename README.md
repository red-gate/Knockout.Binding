Chromium Embedded with Knockout.js Bindings
=========

This repo contains a hacked together experiment to automatically generate Knockout.js ViewModels from a C# ViewModel and to keep the JS and C# view models in sync.

There is an extension method that allows you to bind any C# viewmodel to a CEF frame which will then use a mix of hacked together reflection and event handlers to have any changes in C# update the Js and vice versa.

This is purely a POC and should not be used in production.
