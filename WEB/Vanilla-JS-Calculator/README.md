# Calculatrice

Calculatrice web moderne, réalisée en HTML, CSS et JavaScript vanilla dans le cadre du portfolio de Hamza Grigah.

## Aperçu

Interface sombre, clavier tactile, prise en charge complète du clavier physique et historique des calculs. Aucune dépendance, aucun build : trois fichiers suffisent.

## Fichiers

```text
Calculatrice/
├── Calculatrice.html   Structure de la page
├── Calculatrice.css    Thème et mise en page
└── Calculatrice.js     Saisie, parseur et historique
```

## Lancer la calculatrice

Ouvrez `Calculatrice.html` dans un navigateur, ou servez le dossier :

```powershell
python -m http.server 8000
```

Puis rendez-vous sur `http://localhost:8000/Calculatrice.html`.

## Fonctionnalités

- **Opérations** : addition, soustraction, multiplication, division, parenthèses et pourcentage postfixe (`50 %` vaut `0,5`).
- **Aucun `eval`** : les expressions sont analysées par un parseur récursif descendant, puis évaluées avec des nombres. Une chaîne saisie ne peut donc jamais être exécutée comme du code.
- **Saisie guidée** : remplacement des opérateurs successifs, un seul séparateur décimal par nombre, suppression des zéros inutiles, multiplication implicite (`2(3+4)`).
- **Aperçu en direct** : le résultat s'affiche au-dessus de l'expression pendant la frappe.
- **Clavier physique** : chiffres, `+ - * / x % ( )`, `Entrée` ou `=` pour calculer, `Retour arrière` pour corriger, `Échap` pour tout effacer.
- **Historique** : les huit derniers calculs, effaçables en un clic.
- **Gestion des erreurs** : division par zéro, parenthèses non fermées, expression incomplète ou caractère invalide.
- **Précision** : résultat arrondi à 12 chiffres significatifs pour éviter les artefacts flottants (`0,1 + 0,2 = 0,3`).
- **Accessibilité** : libellés `aria-label`, résultat annoncé via `aria-live`, focus visible, contrastes élevés.
- **Responsive** : l'historique passe sous la calculatrice sur petit écran ; animations désactivées si `prefers-reduced-motion`.

## Choix techniques

| Sujet | Décision |
| --- | --- |
| Évaluation | Parseur récursif descendant (`expression → terme → facteur → primaire`) |
| Pourcentage | Opérateur postfixe : `x %` divise `x` par 100 |
| Formatage | `Intl.NumberFormat("fr-FR")` pour les milliers et la virgule décimale |
| État | Trois variables (`expression`, `dernierResultat`, `mode`) et un tableau d'historique |
| Sécurité | Aucun `eval`, aucune injection HTML (création des nœuds via `createElement`) |

## Auteur

Hamza Grigah — [github.com/grigah-coder](https://github.com/grigah-coder)
