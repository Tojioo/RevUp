# 4. Card Sorting: Navigation & Struktur

*Dokument 9 von 12 · [← 08 Personas](08-PERSONAS.md) · Weiter: [10 Wireframes →](10-wireframes.html)*

Alle Funktionen aus dem [Product Backlog](../01-BACKLOG.md), sortiert nach Nutzerabsicht statt nach Technik, ergeben vier Gruppen.

| Gruppe             | Enthält                                                                | Nutzerabsicht                                       |
|--------------------|------------------------------------------------------------------------|-----------------------------------------------------|
| **Entdecken**      | Meet-Übersicht, Filter nach Thema, Meet-Detailansicht                  | «Ich suche ein Meet.»                               |
| **Erstellen**      | Neues Meet anlegen                                                     | «Ich will ein Meet organisieren.»                   |
| **Meine Meets**    | Eigene Meets verwalten, Anfragen annehmen/ablehnen, bearbeiten/absagen | «Was läuft bei meinen Meets?»                       |
| **Meine Anfragen** | Eigene Beitrittsanfragen und deren Status                              | «Wie steht es um meine Anmeldung?»                  |

- Die Meet-Detailseite ist kein Menüpunkt, sondern wird immer aus «Entdecken» erreicht.
- «Meine Meets» und «Meine Anfragen» bleiben getrennt, auch wenn eine Person beides sein kann: Verwalten und Verfolgen sind verschiedene Aufgaben.

```
RevUp
├── Entdecken        (Startseite, Meet-Liste + Filter)
│   └── Meet-Detail   (kontextuell, kein Menüpunkt)
├── Erstellen         (neues Meet)
├── Meine Meets       (Veranstalter-Sicht)
└── Meine Anfragen    (Teilnehmer-Sicht)
```

Umgesetzt sind `/meets`, `/meets/{id}` und `/meets/create`. Bis «Meine Meets» (US-11) kommt, liegen die Veranstalter-Seiten unter `/meets/{id}/anfragen` und `/meets/{id}/bearbeiten`.