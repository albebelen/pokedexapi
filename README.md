# Pokedex 

Pokedex is a 2-endpoint API written in C# with ASP.NET Core. 

## installation

To install this API and test it, open a new command line window and navigate to your preferred folder.

Create a new directory using the following commands:

```bash
mkdir pokedex
git clone https://github.com/albebelen/pokedexapi.git
```

Once the repo is cloned, run:

```bash
code -r .
```

This will open the code on VS CODE.

Before running the app, open the terminal window on VS CODE and run:
```bash
dotnet restore # this will download and install all the dependencies the project uses
dotnet run # this will build the project and runs it
```

## further considerations

If this were a production API, I'd do the following things

- better error-handling, especially if the name of the pokemon passed to the endpoints doesn't exist - a message would be shown to inform the user that the pokemon they are looking for can't be found, instead of returning an empty object.
- give a list of all the pokemons to the user and give the user the possibility to chose which one to view in detail
- the description of a pokemon is retrieved by getting the first english description found in the flavor_text array, this could have been implemented by giving the user the possibility to chose which version to view and put together all the entries associated to that version. 
- to give the possibility for a user to know all the details about their favorite pokemon and increase the possibility of them using the API to consult during the games, the GET endpoint would get all the information available.
