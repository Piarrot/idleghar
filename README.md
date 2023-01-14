# Idleghar

Idleghar is an idle game with a high-fantasy-RPG style. The player controls a character who can perform quests and earn rewards. It does not need player interaction, except when starting a new quest, equipping the character with the rewarded items and/or crafting and buying items in the store.

- [Game Development Document (GDD)](https://docs.google.com/document/d/1loOzBcBmZVcGhZWQc69hp5oNjqyMfc-GvOeIxjzq_9M/edit?usp=sharing)

## Objective

The objective of this project is to demonstrate techniques and best practices in the development of a mobile and web game with a backend regulating its operations for multiple users.

## Structure

- **IdlegharDotnet/**
  - **IdlegharDotnetDomain**: Domain project with all of the game logic following clean architecture principles. Interactions happen through use cases, entities handle state and logic and providers declare interfaces (ports) for services that are not an integral part of the domain.
  - **IdlegharDotnetBackend**: This is an ASPCore.net project that represents the backend of the game. It has all the implementation details concerning the web interface and the real implementations of the domain providers (adapters).
  - **IdlegharDotnetShared**: This c# class library, hosting shared Request, Response, and View Models that are referenced by both _IdlegharDotnetBackend_ and _IdlegharUnityFrontend_
- **IdlegharUnityFrontend**: Unity3D project for the game frontend, for both mobile and web builds

Tests are integrated in every project and managed with the NUnitFramework. `MockProviders` directory's classes implement simple in-memory alternatives to the more complex providers.

### Whats up with the verbose project names?

When I'm done building this project in C#, I'll re-implement it using Typescript in the same repo, so I needed really specific names for the directories.

Some of the reasons for this are:

- to demonstrate the techniques and best practices of both ecosystems and to show how they relate/deviate,
- to be able to easily mix and match the c# backend with the typescript frontend or the typescript backend with the c#(unity) frontend for experimentation purposes,
- and so we can easily view, measure and analyze the pros and cons of using either of these stacks
