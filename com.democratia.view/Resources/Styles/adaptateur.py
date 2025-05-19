import json
import os

if __name__ == "__main__" :
    try:
        with open("material-theme.json", "r") as fichierMaterial:
            contenu = fichierMaterial.read()
            material = json.loads(contenu)
            fichierMaterial.close()
            for nomTheme in material["schemes"].keys() :
                with open(f"{nomTheme}.xml","w") as fichierTheme:
                    fichierTheme.write(f"""<?xml version="1.0" encoding="utf-8" ?>
<?xaml-comp compile="true" ?> 
<resources>
 \n""")
                    for nomCouleur,valeurCouleur in material["schemes"][nomTheme].items():
                        fichierTheme.write(f"""\t\t<color name="{nomTheme+"-"+nomCouleur}">{valeurCouleur}</color> \n""")
                    fichierTheme.write(f"""</resources>""")
                    fichierTheme.close()
    except FileNotFoundError as e:
        print("Erreur de fichier",e.__repr__())