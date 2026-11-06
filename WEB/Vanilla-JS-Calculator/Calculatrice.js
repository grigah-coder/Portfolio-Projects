/**
 * Calculatrice — logique sans `eval`.
 *
 * Les expressions sont analysées par un parseur récursif descendant
 * (expression → terme → facteur → primaire). Aucune chaîne saisie par
 * l'utilisateur n'est exécutée comme du code.
 */
(() => {
  "use strict";

  const screen = document.querySelector(".screen");
  const displayExpression = document.getElementById("expression");
  const displayResult = document.getElementById("result");
  const hint = document.getElementById("hint");
  const keypad = document.getElementById("keypad");
  const historyList = document.getElementById("history-list");
  const historyEmpty = document.getElementById("history-empty");
  const clearHistoryButton = document.getElementById("clear-history");

  const OPERATEURS = new Set(["+", "-", "*", "/"]);
  const HISTORIQUE_MAX = 8;
  const formateur = new Intl.NumberFormat("fr-FR", {
    maximumFractionDigits: 10,
  });

  /** Expression en cours de saisie (symboles internes : + - * / % ( )). */
  let expression = "";
  /** Dernier résultat validé, ou null en cours de saisie. */
  let dernierResultat = null;
  /** Expression d'origine affichée au-dessus du résultat. */
  let expressionCalculee = "";
  /** "saisie" pendant l'édition, "resultat" après « = ». */
  let mode = "saisie";

  const historique = [];

  /* ---------------------------------------------------------------------
     Parseur
     --------------------------------------------------------------------- */

  class ErreurCalcul extends Error {}

  const estChiffre = (caractere) =>
    caractere.length === 1 && caractere >= "0" && caractere <= "9";

  function tokeniser(source) {
    const jetons = [];
    let index = 0;

    while (index < source.length) {
      const caractere = source[index];

      if (caractere === " ") {
        index += 1;
        continue;
      }

      if (estChiffre(caractere) || caractere === ".") {
        let texte = "";
        let points = 0;

        while (
          index < source.length &&
          (estChiffre(source[index]) || source[index] === ".")
        ) {
          if (source[index] === ".") {
            points += 1;
            if (points > 1) {
              throw new ErreurCalcul("Nombre invalide.");
            }
          }
          texte += source[index];
          index += 1;
        }

        if (texte === ".") {
          throw new ErreurCalcul("Nombre invalide.");
        }

        jetons.push({ type: "nombre", valeur: Number(texte) });
        continue;
      }

      if (OPERATEURS.has(caractere)) {
        jetons.push({ type: "operateur", valeur: caractere });
        index += 1;
        continue;
      }

      if (caractere === "%") {
        jetons.push({ type: "pourcent" });
        index += 1;
        continue;
      }

      if (caractere === "(" || caractere === ")") {
        jetons.push({ type: caractere });
        index += 1;
        continue;
      }

      throw new ErreurCalcul(`Caractère inattendu : « ${caractere} ».`);
    }

    return jetons;
  }

  function evaluer(source) {
    if (!source.trim()) {
      throw new ErreurCalcul("Expression vide.");
    }

    const jetons = tokeniser(source);
    let position = 0;

    const voir = () => jetons[position];
    const consommer = () => jetons[position++];

    function analyserExpression() {
      let valeur = analyserTerme();

      while (
        voir() &&
        voir().type === "operateur" &&
        (voir().valeur === "+" || voir().valeur === "-")
      ) {
        const operateur = consommer().valeur;
        const droite = analyserTerme();
        valeur = operateur === "+" ? valeur + droite : valeur - droite;
      }

      return valeur;
    }

    function analyserTerme() {
      let valeur = analyserFacteur();

      while (voir()) {
        const jeton = voir();

        // Multiplication implicite : 2(3+4) vaut 2*(3+4).
        if (jeton.type === "(") {
          valeur *= analyserFacteur();
          continue;
        }

        if (
          jeton.type !== "operateur" ||
          (jeton.valeur !== "*" && jeton.valeur !== "/")
        ) {
          break;
        }

        const operateur = consommer().valeur;
        const droite = analyserFacteur();

        if (operateur === "/" && droite === 0) {
          throw new ErreurCalcul("Division par zéro.");
        }

        valeur = operateur === "*" ? valeur * droite : valeur / droite;
      }

      return valeur;
    }

    function analyserFacteur() {
      let valeur = analyserPrimaire();

      // Le pourcentage est un opérateur postfixe : 50 % vaut 0,5.
      while (voir() && voir().type === "pourcent") {
        consommer();
        valeur /= 100;
      }

      return valeur;
    }

    function analyserPrimaire() {
      const jeton = voir();

      if (!jeton) {
        throw new ErreurCalcul("Expression incomplète.");
      }

      if (jeton.type === "nombre") {
        consommer();
        return jeton.valeur;
      }

      if (jeton.type === "(") {
        consommer();
        const valeur = analyserExpression();

        if (!voir() || voir().type !== ")") {
          throw new ErreurCalcul("Parenthèse non fermée.");
        }

        consommer();
        return valeur;
      }

      if (
        jeton.type === "operateur" &&
        (jeton.valeur === "-" || jeton.valeur === "+")
      ) {
        consommer();
        const valeur = analyserFacteur();
        return jeton.valeur === "-" ? -valeur : valeur;
      }

      throw new ErreurCalcul("Expression invalide.");
    }

    const resultat = analyserExpression();

    if (position !== jetons.length) {
      throw new ErreurCalcul("Expression invalide.");
    }

    if (!Number.isFinite(resultat)) {
      throw new ErreurCalcul("Résultat hors limites.");
    }

    return resultat;
  }

  /* ---------------------------------------------------------------------
     Formatage
     --------------------------------------------------------------------- */

  /** Supprime les artefacts flottants (0,1 + 0,2) et le zéro négatif. */
  function arrondir(valeur) {
    const arrondi = Number.parseFloat(valeur.toPrecision(12));
    return Object.is(arrondi, -0) ? 0 : arrondi;
  }

  function formater(valeur) {
    return formateur.format(valeur);
  }

  /** Convertit un nombre en texte réutilisable par le parseur. */
  function nombreEnTexte(valeur) {
    if (valeur === 0) {
      return "0";
    }

    const absolu = Math.abs(valeur);
    if (absolu >= 1e15 || absolu < 1e-6) {
      return valeur.toFixed(12).replace(/\.?0+$/, "");
    }

    return String(valeur);
  }

  /** Affiche une expression avec les symboles visuels (×, ÷, −). */
  function afficherExpression(source) {
    return source.replaceAll("*", "×").replaceAll("/", "÷").replaceAll("-", "−");
  }

  function apercu() {
    if (!expression) {
      return null;
    }

    try {
      return arrondir(evaluer(expression));
    } catch {
      return null;
    }
  }

  /* ---------------------------------------------------------------------
     Saisie
     --------------------------------------------------------------------- */

  const dernierCaractere = () => expression.slice(-1);
  const estOperateur = (caractere) => OPERATEURS.has(caractere);

  function segmentCourant() {
    return expression.split(/[+\-*/()%]/).pop() ?? "";
  }

  function ajouter(valeur) {
    // Après « = », un chiffre repart de zéro, un opérateur continue le résultat.
    if (mode === "resultat") {
      const continuer =
        estOperateur(valeur) || valeur === "%" || valeur === ")";

      if (continuer) {
        expression = nombreEnTexte(dernierResultat) + valeur;
      } else {
        expression = valeur === "." ? "0." : valeur;
      }

      mode = "saisie";
      dernierResultat = null;
      expressionCalculee = "";
      rendre();
      return;
    }

    if (estOperateur(valeur)) {
      if (expression === "") {
        if (valeur === "-") {
          expression = "-";
        }
        rendre();
        return;
      }

      const dernier = dernierCaractere();

      if (dernier === "(") {
        if (valeur === "-") {
          expression += "-";
        }
        rendre();
        return;
      }

      // Remplace un opérateur déjà présent au lieu de l'empiler.
      if (estOperateur(dernier)) {
        expression = expression.slice(0, -1) + valeur;
      } else {
        expression += valeur;
      }

      rendre();
      return;
    }

    if (valeur === ".") {
      const dernier = dernierCaractere();

      if (dernier === ")") {
        return;
      }

      const segment = segmentCourant();
      if (segment.includes(".")) {
        return;
      }

      expression += segment === "" ? "0." : ".";
      rendre();
      return;
    }

    if (valeur === "(") {
      const dernier = dernierCaractere();

      // Multiplication implicite : 2(3) devient 2*(3).
      if (estChiffre(dernier) || dernier === ")" || dernier === "%") {
        expression += "*";
      }

      expression += "(";
      rendre();
      return;
    }

    if (valeur === ")") {
      const ouverts = (expression.match(/\(/g) || []).length;
      const fermes = (expression.match(/\)/g) || []).length;
      const dernier = dernierCaractere();

      if (ouverts <= fermes) {
        return;
      }

      if (!(estChiffre(dernier) || dernier === ")" || dernier === "%")) {
        return;
      }

      expression += ")";
      rendre();
      return;
    }

    if (valeur === "%") {
      const dernier = dernierCaractere();

      if (estChiffre(dernier) || dernier === ")" || dernier === "%") {
        expression += "%";
        rendre();
      }
      return;
    }

    if (estChiffre(valeur)) {
      if (dernierCaractere() === ")") {
        expression += `*${valeur}`;
        rendre();
        return;
      }

      // Évite les zéros inutiles en tête de nombre (007).
      if (segmentCourant() === "0") {
        expression = expression.slice(0, -1) + valeur;
      } else {
        expression += valeur;
      }

      rendre();
    }
  }

  function effacerDernier() {
    if (mode === "resultat") {
      expression = nombreEnTexte(dernierResultat);
      mode = "saisie";
      dernierResultat = null;
      expressionCalculee = "";
    }

    expression = expression.slice(0, -1);
    rendre();
  }

  function toutEffacer() {
    expression = "";
    dernierResultat = null;
    expressionCalculee = "";
    mode = "saisie";
    rendre();
  }

  function calculer() {
    if (!expression) {
      return;
    }

    try {
      const valeur = arrondir(evaluer(expression));

      ajouterHistorique(expression, valeur);
      expressionCalculee = expression;
      dernierResultat = valeur;
      mode = "resultat";
      rendre();
    } catch (erreur) {
      const message =
        erreur instanceof ErreurCalcul
          ? erreur.message
          : "Calcul impossible.";

      screen.classList.add("is-error");
      displayExpression.textContent = afficherExpression(expression);
      displayResult.textContent = "Erreur";
      displayResult.classList.remove("is-preview");
      hint.textContent = message;
    }
  }

  /* ---------------------------------------------------------------------
     Rendu
     --------------------------------------------------------------------- */

  function faireDefiler(element) {
    element.scrollLeft = element.scrollWidth;
  }

  function rendre() {
    screen.classList.remove("is-error");

    if (mode === "resultat") {
      displayExpression.textContent = `${afficherExpression(expressionCalculee)} =`;
      displayResult.textContent = formater(dernierResultat);
      displayResult.classList.remove("is-preview");
      hint.textContent = "Un chiffre pour recommencer, un opérateur pour continuer";
    } else {
      const valeur = apercu();
      const contientOperation = /[+\-*/()%]/.test(expression.replace(/^-/, ""));

      displayExpression.textContent =
        valeur !== null && contientOperation ? `= ${formater(valeur)}` : "";
      displayResult.textContent = expression ? afficherExpression(expression) : "0";
      displayResult.classList.remove("is-preview");
      hint.textContent = expression
        ? "Entrée pour calculer · Retour arrière pour corriger"
        : "Tapez une opération ou utilisez le clavier";
    }

    faireDefiler(displayExpression);
    faireDefiler(displayResult);

    if (mode === "resultat") {
      faireDefiler(displayResult);
    }
  }

  /* ---------------------------------------------------------------------
     Historique
     --------------------------------------------------------------------- */

  function ajouterHistorique(source, valeur) {
    historique.unshift({ source, valeur });

    if (historique.length > HISTORIQUE_MAX) {
      historique.pop();
    }

    rendreHistorique();
  }

  function rendreHistorique() {
    const elements = historique.map((entree) => {
      const item = document.createElement("li");
      item.className = "history__item";

      const source = document.createElement("span");
      source.className = "history__expression";
      source.textContent = afficherExpression(entree.source);

      const valeur = document.createElement("span");
      valeur.className = "history__value";
      valeur.textContent = formater(entree.valeur);

      item.append(source, valeur);
      return item;
    });

    historyList.replaceChildren(...elements);
    historyEmpty.hidden = historique.length > 0;
    clearHistoryButton.disabled = historique.length === 0;
  }

  function viderHistorique() {
    historique.length = 0;
    rendreHistorique();
  }

  /* ---------------------------------------------------------------------
     Événements
     --------------------------------------------------------------------- */

  keypad.addEventListener("click", (evenement) => {
    const bouton = evenement.target.closest("button");
    if (!bouton) {
      return;
    }

    if (bouton.dataset.action === "clear") {
      toutEffacer();
    } else if (bouton.dataset.action === "backspace") {
      effacerDernier();
    } else if (bouton.dataset.action === "equals") {
      calculer();
    } else if (bouton.dataset.value) {
      ajouter(bouton.dataset.value);
    }
  });

  document.addEventListener("keydown", (evenement) => {
    if (evenement.ctrlKey || evenement.metaKey || evenement.altKey) {
      return;
    }

    // Preserve native keyboard activation when a control has focus.
    if (
      evenement.target instanceof Element &&
      evenement.target.closest("button, a, input, textarea, select, [contenteditable='true']")
    ) {
      return;
    }

    const touche = evenement.key;

    if (estChiffre(touche)) {
      ajouter(touche);
    } else if (touche === "." || touche === ",") {
      ajouter(".");
    } else if (OPERATEURS.has(touche)) {
      ajouter(touche);
    } else if (touche === "x" || touche === "X") {
      ajouter("*");
    } else if (touche === "%") {
      ajouter("%");
    } else if (touche === "(" || touche === ")") {
      ajouter(touche);
    } else if (touche === "Enter" || touche === "=") {
      calculer();
    } else if (touche === "Backspace") {
      effacerDernier();
    } else if (touche === "Escape" || touche === "Delete") {
      toutEffacer();
    } else {
      return;
    }

    evenement.preventDefault();
  });

  clearHistoryButton.addEventListener("click", viderHistorique);

  rendreHistorique();
  rendre();
})();
