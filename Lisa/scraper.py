from urllib.request import urlopen
from json import loads
from bs4 import BeautifulSoup
import pickle

alcoholic_drinks_url = 'https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Alcoholic'
non_alcoholic_drinks_url = 'https://www.thecocktaildb.com/api/json/v1/1/filter.php?a=Non_Alcoholic'
id_lookup_page = 'https://www.thecocktaildb.com/api/json/v1/1/lookup.php?i='


def get_alcoholic_drink_ids(url):
    alcoholic_drinks = urlopen(url)
    alcoholic_drinks = BeautifulSoup(alcoholic_drinks, "html5lib")
    alcoholic_drinks = loads(alcoholic_drinks.body.string)['drinks']

    drink_ids = []

    print("Scraping alcoholic drink IDs...")

    # save all alcoholic drink ids for recipe lookup
    for drink in alcoholic_drinks:
        print('scraped id: ' + str(drink['idDrink']))
        drink_ids.append(drink['idDrink'])

    return drink_ids


def get_non_alcoholic_drink_ids(url):

    # Get non-alcoholic drinks
    non_alcoholic_drinks = urlopen(url)
    non_alcoholic_drinks = BeautifulSoup(non_alcoholic_drinks, "html5lib")
    non_alcoholic_drinks = loads(non_alcoholic_drinks.body.string)['drinks']

    drink_ids = []

    print("Scraping non-alcoholic drink IDs...")

    # save all non-alcoholic drink ids for recipe lookup
    for drink in non_alcoholic_drinks:
        print('scraped id: ' + str(drink['idDrink']))
        drink_ids.append(drink['idDrink'])

    return drink_ids


def get_recipe_list_by_ids(ids):
    """ return a list of dict objects, each a drink recipe with its details"""

    recipes = []

    for drink_id in ids:
        recipe = urlopen(id_lookup_page + str(drink_id))
        recipe = BeautifulSoup(recipe, "html5lib")
        recipe = loads(recipe.body.string)['drinks'][0]

        print("scraped recipe for drink: " + recipe['strDrink'])

        # remove unnecessary keys
        recipe.pop('strVideo')
        recipe.pop('strIBA')
        recipe.pop("strGlass")
        # iterate over a shallow copy because you can't delete while iterating
        for key, val in list(recipe.items()):
            if ("Ingredient" in key or "Measure" in key) and recipe[key] == "":  # delete empty entries
                del recipe[key]

        recipes.append(recipe)

    return recipes


def main():
    alcoholic_drink_ids = get_alcoholic_drink_ids(alcoholic_drinks_url)
    non_alcoholic_drink_ids = get_non_alcoholic_drink_ids(non_alcoholic_drinks_url)

    alcoholic_drinks = get_recipe_list_by_ids(alcoholic_drink_ids)
    non_alcoholic_drinks = get_recipe_list_by_ids(non_alcoholic_drink_ids)

    with open('alcoholic_drinks.pickle', 'wb') as handle:
        pickle.dump(alcoholic_drinks, handle, protocol=pickle.HIGHEST_PROTOCOL)

    with open('non_alcoholic_drinks.pickle', 'wb') as handle:
        pickle.dump(non_alcoholic_drinks, handle, protocol=pickle.HIGHEST_PROTOCOL)

main()
