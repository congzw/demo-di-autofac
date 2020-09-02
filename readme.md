# a demo for autofac di

## Glossary

- *Activator*    Part of a *Registration* that, given a *Context* and a set of *Parameters*, can create a *Component Instance* bound to that *Context*
- *Argument*     A formal argument to a constructor on a .NET type
- *Component*    A body of code that declares the *Services* it provides and the *Dependencies* it consumes
- *Instance*     A .NET object obtained by *Activating* a *Component* that provides *Services* within a *Container* (also *Component Instance*)
- *Container*    A construct that manages the *Components* that make up an application
- *Context*      A bounded region in which a specific set of *Services* is available
- *Dependency*   A *Service* required by a *Component*
- *Lifetime*     A duration bounded by the *Activation* of an *Instance* and its disposal
- *Parameter*    Non-*Service* objects used to configure a *Component*
- *Registration* The act of adding and configuring a *Component* for use in a *Container*, and the information associated with this process
- *Scope*        The specific *Context* in which *Instances* of a *Component* will be shared by other *Components* that depend on their *Services*
- *Service*      A well-defined behavioural contract shared between a providing and a consuming *Component*

## change list

- 20200902 init projects