# Idleghar

Idleghar is an idle game with a high-fantasy-RPG-style. The player controls a character who can perform quests and earn rewards. It does not need player interaction, except when starting a new quest or crafting/buying items in the store.

- [Game Development Document (GDD)](https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit?usp=sharing)

## Objective

The objective of this project is to demonstrate techniques and best practices for the development of a mobile and web game with a backend regulating its operations.

## Structure

- **IdlegharDotnet/**
  - **IdlegharDotnetBackend**: This is a ASPCore.net project that represents the backend of the game. In the `Domain` directory you will find all the use cases.
  - **IdlegharDotnetBackendTests**: Tests for _IdlegharDotnetBackend_ are hosted in this NUnitFramework project. `MockProviders` directory's classes implement simple in-memory alternatives to the more complex or io-bound providers of _IdlegharDotnetBackend_ domain, for testing purposes.
  - **IdlegharDotnetShared**: This c# class library, hosts shared Request and Response Models that are referenced by both _IdlegharDotnetBackend_ and _IdlegharUnityFrontend_
- **IdlegharUnityFrontend**: Unity3D project for the game frontend, for both mobile and web builds

### Whats up with the verbose project names?

When I'm done building this project in C#, I'll re-implement it using Typescript in the same repo, so I needed really specific names for the directories.

Some of the reasons for this are:

- to demonstrate techniques and best practices of another ecosystem and show how they relate/deviate,
- to be able to easily mix and match the c# backend with the typescript frontend or the typescript backend with the c#(unity) frontend,
- and so we can easily view, measure and analyze the pros and cons of using these stacks
