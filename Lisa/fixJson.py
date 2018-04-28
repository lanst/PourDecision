import json

drinks = {}
Items = []
drinks["Items"] = Items
modifiedDrinks = {}

ingredients = ""
ingredientsCombined = ""

data = json.load(open('nonAlcoholicDrinks.json'))

for item in data["Items"]:
    try:
        for i in range(1, 9):
            ingredients += item["strIngredient" + str(i)] + " "
    except Exception as e:
        error = e

    ingredientsCombined = ingredients[:-1]
    ingredients = ""

    modifiedDrinks["name"] = item["strDrink"].lower()
    modifiedDrinks["ingredients"] = ingredientsCombined.lower()
    modifiedDrinks["instruction"] = item["strInstructions"].lower()
    modifiedDrinks["alcoholic"] = item["strAlcoholic"].lower()

    drinks["Items"].append(modifiedDrinks)
    modifiedDrinks = {}

with open('modifiedNonDrinks.json', 'w') as outfile:
    json.dump(drinks, outfile)
