# Burger House

Page vitrine statique pour un restaurant de hamburgers, réalisée en HTML et CSS dans le cadre du portfolio de Hamza Grigah.

## Aperçu

`Accueil.html` est une page d'accueil complète : en-tête collant, héro, carte des burgers, promotions et section contact. Aucun framework, aucune dépendance : le fichier s'ouvre directement dans un navigateur.

## Structure

```text
Burger/
├── Accueil.html        Page d'accueil (point d'entrée)
├── css/
│   └── css.css         Feuille de styles unique
└── images/             Photos des burgers (PNG, fond transparent)
```

## Lancer la page

Ouvrez `Accueil.html` dans un navigateur, ou servez le dossier localement :

```powershell
# Python
python -m http.server 8000

# Node.js
npx serve .
```

Puis rendez-vous sur `http://localhost:8000/Accueil.html`.

## Fonctionnalités

- **Mise en page responsive** : héro en deux colonnes sur ordinateur, empilé sur mobile ; grille de cartes fluide (`auto-fit` / `minmax`).
- **Design tokens** : couleurs, rayons, ombres et espacements centralisés dans `:root`.
- **Navigation** : ancres vers les sections Accueil, Menu, Promotions et Contact, avec compensation de l'en-tête collant (`scroll-padding-top`).
- **Cartes produits** : image, badge, description, prix et appel à l'action.
- **Accessibilité** : lien d'évitement, `alt` sur les images, contrastes soignés, focus visible, structure sémantique (`header`, `main`, `section`, `footer`).
- **Animations discrètes** : survols et transitions, désactivés si `prefers-reduced-motion` est actif.
- **Performance** : images dimensionnées (`width` / `height`), chargement différé sous la ligne de flottaison, image du héro prioritaire.

## Personnalisation

| Élément | Où le modifier |
| --- | --- |
| Couleurs, rayons, ombres | `:root` dans `css/css.css` |
| Produits et prix | section `#Menu` dans `Accueil.html` |
| Promotions | section `#Promotions` |
| Coordonnées | section `#Contact` |

## Auteur

Hamza Grigah — [github.com/grigah-coder](https://github.com/grigah-coder)
