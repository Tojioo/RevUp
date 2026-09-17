# Prototyp

*Letztes Dokument der Kette · [← 11 Prototyp-Test](../11-PROTOTYP-TEST.md) · [↑ Zurück zum Start](../../../README.md)*

Hi-Fi-Prototyp der 4 Hauptscreens, aufbauend auf [`10-wireframes.html`](../10-wireframes.html) und den Tokens aus [`04-DESIGN.md`](../../Design/04-DESIGN.md).

## Primäre Version: `prototype.html`

**[`prototype.html`](prototype.html)** ist der massgebliche, eigenständige Prototyp: reines HTML/CSS/JS, keine Abhängigkeiten ausser Google Fonts, läuft lokal im Browser (Datei direkt öffnen). Alle 4 Screens sind klickbar:

- **Entdecken**: Themen-Filter filtert die Liste live, Leerzustand bei 0 Treffern.
- **Erstellen**: Zeitmodus/Sichtbarkeit als Segmented Control, Teilnehmer-/Lautstärke-Stepper.
- **Meet-Detail**: "Beitreten" (öffentlich) bzw. "Anfrage stellen" (privat) ändert Status-Pill und Button live. Umschalter oben im Screen wechselt zwischen den beiden Beispielen.
- **Meine Meets**: Annehmen/Ablehnen pro Anfrage aktualisiert Zeile und offene-Anfragen-Zähler live.

## Interne Version: Claude Design

Dieselben 4 Screens existieren zusätzlich als Claude-Design-Canvas (`.dc.html`-Quellen in diesem Ordner: `Main.dc.html`, `Erstellen.dc.html`, `Detail.dc.html`, `MeineMeets.dc.html`, `canvas.json`). Das ist reine interne Arbeitsversion für visuelles Iterieren (WYSIWYG-Editor), **nicht geteilt, kein öffentlicher Link**. Die massgebliche, referenzierbare Version für die Doku ist `prototype.html` oben.

## Design-Entscheidungen gegenüber 04-DESIGN.md

- Typografie: Big Shoulders Display (Headlines), Manrope (UI/Body), IBM Plex Mono (Meta/Daten) durchgängig übernommen.
- Layout-Muster an Apple HIG angelehnt (plattformunabhängig, nicht iOS-spezifisch): Large-Title-Header, Segmented Controls, Tab-Bar, Inset-Grouped-Formularfelder, Stepper. Kein Fake-Statusbar/-Keyboard.
- Tap-Ziele durchgängig auf mind. 44×44px (Stepper, Zurück-Button, Annehmen/Ablehnen, Themen-Chips, Segmented-Control-Optionen).
- Akzentfarbe (Mint) bewusst nur für Interaktives reserviert (Buttons, aktive Zustände). Der "Öffentlich"-Badge nutzt stattdessen Info-Blau (`#4DA3FF`) statt Mint, um Marken-Akzent und Kategorie-Label sauber zu trennen. Siehe `04-DESIGN.md`, Abschnitt "Interaktion & Zustände".
