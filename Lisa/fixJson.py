import json

drinks = {}
Items = []
drinks["Items"] = Items
modifiedDrinks = {}
ingrd = []

ingredients = ""
ingredientsCombined = ""

data = json.load(open('drinks.json'))

for item in data["Items"]:
    try:
        for i in range(1, 9):
            ingrd.append(item["strIngredient" + str(i)] + ",")
    except Exception as e:
        error = e

    ingrd = sorted(ingrd)

    for element in ingrd:
        ingredients += element

    ingredientsCombined = ingredients[:-1]
    ingredients = ""
    ingrd = []

    modifiedDrinks["name"] = item["strDrink"].lower()
    modifiedDrinks["ingredients"] = ingredientsCombined.lower()
    modifiedDrinks["instruction"] = item["strInstructions"].lower()
    modifiedDrinks["alcoholic"] = item["strAlcoholic"].lower()

    drinks["Items"].append(modifiedDrinks)
    modifiedDrinks = {}

with open('modifiedDrinks.json', 'w') as outfile:
    json.dump(drinks, outfile)
