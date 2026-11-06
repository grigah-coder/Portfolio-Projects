# Portail CEDOC — Centre des Études Doctorales

Site vitrine statique du Centre des Études Doctorales de l'Université Ibn Tofaïl (Kénitra), réalisé en HTML, CSS et JavaScript avec Bootstrap 5 et Font Awesome 6 dans le cadre du portfolio de Hamza Grigah.

## Aperçu

Six pages cohérentes, sans build ni serveur applicatif : navigation partagée, en-têtes de page, cartes de services, listes de procédure et page de documents avec recherche instantanée.

| Page | Fichier | Contenu |
| --- | --- | --- |
| Accueil | `index.html` | Présentation du Centre et des quatre services |
| Inscription en doctorat | `inscription.html` | Étapes et pièces à fournir |
| Réinscription | `reinscription.html` | Renouvellement annuel de l'inscription |
| Soutenance de thèse | `soutenance.html` | Dossier de soutenance et procédure |
| Documents utiles | `documents.html` | Formulaires téléchargeables + recherche |
| Contact | `contact.html` | Adresse, téléphone, e-mail et horaires |

## Structure

```text
cedoc-porta/
├── index.html
├── inscription.html
├── reinscription.html
├── soutenance.html
├── documents.html
├── contact.html
├── css/
│   ├── bootstrap.min.css   Bootstrap 5 (fourni)
│   └── style.css           Thème, composants et accessibilité
├── js/
│   ├── bootstrap.bundle.min.js
│   └── main.js             Menu actif, fermeture mobile, recherche
├── webfonts/
│   └── fontawesome6/       Font Awesome 6 (fourni)
└── assets/                 Logo et documents administratifs
```

## Lancer le site

Un simple serveur statique suffit (les liens `download` et les chemins relatifs fonctionnent mieux qu'en `file://`) :

```powershell
python -m http.server 8000
```

Puis ouvrir `http://localhost:8000/index.html`.

## Fonctionnalités

- **Navigation cohérente** : barre fixe, page active détectée automatiquement à partir de l'URL, fermeture du menu mobile au clic sur un lien.
- **En-têtes de page** : `.page-hero` pour l'accueil, variante `.page-hero--compact` pour les pages internes.
- **Recherche de documents** : filtrage instantané insensible aux accents et à la casse, compteur de résultats, état vide avec bouton de réinitialisation, raccourci `/` pour cibler le champ et `Échap` pour l'effacer.
- **Documents** : chaque carte affiche un badge de type de fichier (`PDF`, `DOCX`, `PPTX`) et un lien de téléchargement direct.
- **Design tokens** : couleurs, rayons, ombres et transitions centralisés dans `:root` (`css/style.css`).
- **Responsive** : grilles Bootstrap, hero fluide, tableaux de définition empilés sur mobile, gestion des écrans ≤ 360 px.
- **Accessibilité** : lien d'évitement, `main#contenu` focusable, libellés `aria-label` / `aria-current` / `aria-expanded`, `aria-live` sur le compteur de recherche, focus visible, contrastes vérifiés.
- **Impression** : navigation, boutons, recherche et alertes masqués ; contenu et cartes conservés.
- **Animations réduites** : toutes les transitions sont neutralisées si `prefers-reduced-motion` est actif.

## Recherche de documents

`js/main.js` indexe une seule fois le texte de chaque `.searchable-item`, puis compare la saisie normalisée (minuscules, accents retirés) au contenu indexé. Les éléments non correspondants reçoivent la classe `.is-hidden`, et une section dont tous les documents sont masqués disparaît entièrement.

| Élément | Rôle |
| --- | --- |
| `#searchInput` | Champ de recherche |
| `#searchStatus` | Compteur `aria-live` (« 3 résultats sur 12 ») |
| `#searchEmpty` | État vide + bouton de réinitialisation |
| `#resetSearch` | Vide la recherche et redonne le focus au champ |

## Personnalisation

| Élément | Où le modifier |
| --- | --- |
| Couleurs, rayons, ombres | bloc `:root` dans `css/style.css` |
| Titre du site et logo | `.navbar-brand` dans chaque page |
| Documents listés | sections `.documents-section` dans `documents.html` |
| Coordonnées | page `contact.html` |
| Comportement de la recherche | `initDocumentSearch()` dans `js/main.js` |

## Auteur

Hamza Grigah — [github.com/grigah-coder](https://github.com/grigah-coder)
