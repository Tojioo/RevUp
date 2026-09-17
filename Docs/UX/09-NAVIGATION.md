# 4. Card Sorting: Navigation & Struktur

*Dokument 9 von 11 · [← 08 Personas](08-PERSONAS.md) · Weiter: [10 Wireframes →](10-wireframes.html)*

Ausgangspunkt waren alle Funktionen aus dem MVP-Backlog (siehe [`01-BACKLOG.md`](../01-BACKLOG.md)). Sortiert nach Nutzerabsicht statt nach technischer Umsetzung ergeben sich vier Themengruppen.

## Themengruppen

| Gruppe             | Enthält                                                                | Nutzerabsicht                                       |
|--------------------|------------------------------------------------------------------------|-----------------------------------------------------|
| **Entdecken**      | Meet-Übersicht, Filter nach Thema, Meet-Detailansicht                  | "Ich suche ein Meet."                               |
| **Erstellen**      | Neues Meet anlegen                                                     | "Ich will ein Meet organisieren."                   |
| **Meine Meets**    | Eigene Meets verwalten, Anfragen annehmen/ablehnen, bearbeiten/absagen | "Ich bin Veranstalter, was läuft bei meinen Meets?" |
| **Meine Anfragen** | Eigene Beitrittsanfragen und deren Status                              | "Ich habe mich angemeldet, wie ist der Stand?"      |

## Hierarchie

- Meet-Detail ist keine eigene Navigationsebene, sondern wird immer aus "Entdecken" heraus erreicht (kein direkter Menüpunkt).
- "Meine Meets" und "Meine Anfragen" bleiben getrennt, auch wenn dieselbe Person theoretisch beides sein kann (Veranstalter und Teilnehmer). Grund: die Aufgaben sind unterschiedlich (verwalten vs. verfolgen), eine Vermischung würde die Übersicht unübersichtlich machen.

## Resultierende Hauptnavigation

```
RevUp
├── Entdecken        (Startseite, Meet-Liste + Filter)
│   └── Meet-Detail   (kontextuell, kein Menüpunkt)
├── Erstellen         (neues Meet)
├── Meine Meets       (Veranstalter-Sicht)
└── Meine Anfragen    (Teilnehmer-Sicht)
```

Direkte Übersetzung in Blazor-Routen: `/meets`, `/meets/{id}`, `/meets/create`, `/meets/mine`, `/requests/mine`.