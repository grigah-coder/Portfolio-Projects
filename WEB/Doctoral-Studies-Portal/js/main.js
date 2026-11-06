/* ==========================================================================
   PORTAIL CEDOC - SCRIPTS PRINCIPAUX
   JavaScript Vanilla (ES2020), sans dépendance autre que Bootstrap pour le
   menu mobile. Chaque module est autonome et sort silencieusement si les
   éléments qu'il pilote ne sont pas présents sur la page courante.
   ========================================================================== */

document.addEventListener("DOMContentLoaded", () => {
  setActiveNavLink();
  initNavbarAutoClose();
  initDocumentSearch();
});

/* --------------------------------------------------------------------------
   1. LIEN ACTIF DU MENU
   Compare le nom du fichier de l'URL (ex. « inscription.html ») au href de
   chaque lien, puis applique .active et aria-current="page".
   -------------------------------------------------------------------------- */
function setActiveNavLink() {
  const currentPage = window.location.pathname.split("/").pop() || "index.html";

  document.querySelectorAll(".navbar-nav .nav-link").forEach((link) => {
    const isCurrent = link.getAttribute("href") === currentPage;

    link.classList.toggle("active", isCurrent);
    if (isCurrent) {
      link.setAttribute("aria-current", "page");
    } else {
      link.removeAttribute("aria-current");
    }
  });
}

/* --------------------------------------------------------------------------
   2. FERMETURE AUTOMATIQUE DU MENU MOBILE (BURGER)
   Ferme le collapse Bootstrap quand un lien est cliqué sur mobile.
   Le garde-fou `typeof bootstrap` évite une erreur si le bundle n'a pas pu
   être chargé (fichier manquant, réseau coupé, etc.).
   -------------------------------------------------------------------------- */
function initNavbarAutoClose() {
  const navbarMenu = document.getElementById("navbarNav");
  if (!navbarMenu || typeof bootstrap === "undefined") return;

  const collapseMenu = bootstrap.Collapse.getOrCreateInstance(navbarMenu, {
    toggle: false,
  });

  navbarMenu.querySelectorAll(".nav-link").forEach((link) => {
    link.addEventListener("click", () => {
      // Le menu n'est refermé que s'il est ouvert (utile sur mobile).
      if (navbarMenu.classList.contains("show")) {
        collapseMenu.hide();
      }
    });
  });
}

/* --------------------------------------------------------------------------
   3. RECHERCHE EN TEMPS RÉEL DES DOCUMENTS
   Actif uniquement sur documents.html (#searchInput).

   Fonctionnement :
   - la saisie est normalisée (minuscules, accents retirés) ;
   - chaque .searchable-item est comparé au texte de son contenu ;
   - les éléments non correspondants reçoivent .is-hidden ;
   - une section sans résultat est entièrement masquée ;
   - un compteur et un état vide informent l'utilisateur ;
   - « Échap » ou le bouton « Réinitialiser » vide la recherche ;
   - « / » place le focus dans le champ depuis n'importe où sur la page.
   -------------------------------------------------------------------------- */
function initDocumentSearch() {
  const searchInput = document.getElementById("searchInput");
  if (!searchInput) return;

  const status = document.getElementById("searchStatus");
  const emptyState = document.getElementById("searchEmpty");
  const resetButton = document.getElementById("resetSearch");
  const items = Array.from(document.querySelectorAll(".searchable-item"));
  const sections = Array.from(document.querySelectorAll(".documents-section"));

  if (items.length === 0) return;

  /** Minuscules sans accents, pour une recherche tolérante à la frappe. */
  const normalize = (text) =>
    text
      .toLowerCase()
      .normalize("NFD")
      .replace(/[\u0300-\u036f]/g, "")
      .replace(/\s+/g, " ")
      .trim();

  // Texte indexé une seule fois : la recherche reste fluide même sur une
  // longue liste de documents.
  const index = items.map((item) => ({
    element: item,
    text: normalize(item.textContent || ""),
  }));

  const total = items.length;

  const setStatus = (visibleCount, query) => {
    if (!status) return;

    if (!query) {
      status.textContent = `${total} document${total > 1 ? "s" : ""} disponible${total > 1 ? "s" : ""}.`;
      status.dataset.state = "idle";
      return;
    }

    status.textContent =
      visibleCount === 0
        ? `Aucun résultat pour « ${query} ».`
        : `${visibleCount} résultat${visibleCount > 1 ? "s" : ""} sur ${total} pour « ${query} ».`;
    status.dataset.state = visibleCount === 0 ? "empty" : "results";
  };

  const filterItems = () => {
    const query = normalize(searchInput.value);
    let visibleCount = 0;

    for (const { element, text } of index) {
      const matches = query === "" || text.includes(query);

      element.classList.toggle("is-hidden", !matches);
      if (matches) visibleCount += 1;
    }

    // Une section dont tous les documents sont masqués disparaît entièrement.
    for (const section of sections) {
      const hasVisibleItem = section.querySelector(
        ".searchable-item:not(.is-hidden)"
      );
      section.classList.toggle("is-hidden", !hasVisibleItem);
    }

    if (emptyState) {
      emptyState.hidden = visibleCount !== 0;
    }

    if (resetButton) {
      resetButton.hidden = query === "";
    }

    setStatus(visibleCount, searchInput.value.trim());
  };

  const resetSearch = () => {
    searchInput.value = "";
    filterItems();
    searchInput.focus();
  };

  searchInput.addEventListener("input", filterItems);

  searchInput.addEventListener("keydown", (event) => {
    if (event.key === "Escape" && searchInput.value !== "") {
      event.preventDefault();
      resetSearch();
    }
  });

  if (resetButton) {
    resetButton.addEventListener("click", resetSearch);
  }

  // Raccourci « / » : focus dans la recherche, sauf si l'utilisateur écrit
  // déjà dans un champ ou un contenu éditable.
  document.addEventListener("keydown", (event) => {
    const active = document.activeElement;
    const isTyping =
      active instanceof HTMLElement &&
      (active.matches("input, textarea, select") || active.isContentEditable);

    if (event.key === "/" && !isTyping && !event.ctrlKey && !event.metaKey) {
      event.preventDefault();
      searchInput.focus();
    }
  });

  filterItems();
}
