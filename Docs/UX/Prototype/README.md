# Prototyp

*Zu Dokument 11 · [← 11 Prototyp-Test](../11-PROTOTYP-TEST.md) · Weiter: [12 Lösungsdokument →](../../12-LOESUNGSDOKUMENT.md)*

Hi-Fi-Prototyp der 4 Hauptscreens auf Basis der [Wireframes](../10-wireframes.html) und der [Design-Grundlagen](../../Design/04-DESIGN.md).

**[prototype.html](prototype.html)** ist der massgebliche Prototyp: reines HTML, CSS und JavaScript, läuft lokal im Browser (Datei direkt öffnen). Alle 4 Screens sind klickbar:

- **Entdecken:** Der Themen-Filter filtert die Liste live, mit Leerzustand.
- **Erstellen:** Zeitmodus und Sichtbarkeit als Umschalter, Stepper für Teilnehmer und Lautstärke.
- **Meet-Detail:** «Beitreten» (öffentlich) bzw. «Anfrage stellen» (privat) ändert Status und Button live.
- **Meine Meets:** Annehmen/Ablehnen aktualisiert Zeile und Zähler live.

Die `.dc.html`-Dateien und `canvas.json` sind die Arbeitsdateien aus Claude Design.

## Design-Entscheidungen

- Schriften: Big Shoulders Display (Titel), Manrope (Oberfläche), IBM Plex Mono (Metadaten).
- Layout nach den Human Interface Guidelines, aber plattformunabhängig: grosse Titel, Umschalter, Tab-Bar, gruppierte Formularfelder, Stepper.
- Tap-Ziele mindestens 44 × 44 px.
- Mint nur für Interaktives; das «Öffentlich»-Badge nutzt Info-Blau (`#4DA3FF`), damit Akzent und Kategorie-Label getrennt bleiben.