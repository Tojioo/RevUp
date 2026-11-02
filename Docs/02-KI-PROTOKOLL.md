# KI-Protokoll: RevUp (M426)

*Dokument 2 von 12 · [← 01 Backlog](01-BACKLOG.md) · Weiter: [03 Konzept →](03-KONZEPT.md)*

Festgehalten sind die wichtigsten Anfragen an die KI, sinngemäss und ohne die Antworten.

**Rollen:** Idee, Anforderungen, Prioritäten, Regeln (z. B. `.editorconfig`, Merge nur mit Merge-Commit) und alle Entscheide kamen von mir. Die KI habe ich zum Besprechen von Ideen genutzt, in der IDE laufend für kurze Fragen, Refactorings, Debugging und das Prüfen eigener Änderungen vor dem Commit (nicht einzeln aufgeführt) und ab Mitte Oktober für die Umsetzung der Stories mit Claude Code nach meinen Vorgaben. Jede Umsetzung von Claude Code kam als Pull Request, den ich geprüft und gemergt habe. Mehr dazu in Abschnitt 7 des [Lösungsdokuments](12-LOESUNGSDOKUMENT.md).

| Datum | Anfrage (sinngemäss) |
|---|---|
| 2026-08-20 | Ideen für ein Projekt in M426 sammeln; eigene Idee einer Car-Meet-App vorstellen (öffentlich/privat, Themen, Zeitmodelle, Limits, Polizei-Funktion). |
| 2026-08-20 | Blazor, .NET MAUI und WPF vergleichen (Aufwand, Publishing); Entscheid für Blazor. |
| 2026-08-20 | Unterstützung beim Aufsetzen des Projekts, beim README und beim Product Backlog. |
| 2026-08-22 | Unterstützung beim Icon-Konzept (Squircle, Glyphe, Akzentfarbe). |
| 2026-08-28 | Unterstützung bei den UX-Dokumenten (Fallbeschreibung bis Prototyp-Test). |
| 2026-08-28 | Hi-Fi-Prototyp der 4 Hauptscreens mit Claude Design erstellen (erst statisch, dann klickbar) und nach meinem Feedback überarbeiten (Scrollbar, Farbpalette). |
| 2026-10-10 | Projekt mit den Vorgaben der LB abgleichen; Vorgehen für Board, Branches, TDD und CI festlegen. |
| 2026-10-12 | Technisches Fundament (CI, Vorlagen, Story-IDs), Stories als GitHub-Issues; US-01 (Meet erstellen) umsetzen. |
| 2026-10-13 | Erklärung, wie das Projekt-Board in GitHub Projects eingerichtet wird. |
| 2026-10-15 | US-02 (Sichtbarkeit) und US-03 (Meet-Liste) umsetzen. |
| 2026-10-15 | Code an meine strengere `.editorconfig` anpassen. |
| 2026-10-15 | Technische Vorgaben als T-01 bis T-03 erfassen und umsetzen (Icons, Persistenz mit linq2db/SQLite, async, Unit- und Integrationstests). |
| 2026-10-15 | Rider-Hinweise in den Testprojekten beheben; US-04 (öffentlichem Meet beitreten) umsetzen. |
| 2026-10-15 | Konten besprechen; schlanke Variante als US-17 bis US-19 erfassen, Zwei-Faktor und Passkeys als Stretch (US-20). |
| 2026-10-29 | Planung bis zur Abgabe: zuerst US-05/US-06 mit Observer, danach SOLID, Retrospektive und Abgabe. |
| 2026-10-29 | US-05 (Beitrittsanfrage) und US-06 (Anfragen entscheiden, Observer) umsetzen. |
| 2026-10-29 | Sprint 1 abschliessen; nicht bewertungsrelevante Stories in den Meilenstein «Nach der LB» verschieben; US-10 (freie Plätze) umsetzen. |
| 2026-10-29 | Lösungsdokument entwerfen (Patterns, Branches, TDD-Nachweis, SOLID, Sprint-Reviews, Retrospektive). |
| 2026-10-29 | Sprint 2 planen (Abgabe 02.11.); US-07 (Filter) und US-08 (Zeitmodell) umsetzen. |
| 2026-10-29 | Merge-Konflikte in offenen Pull Requests auflösen; Gesamtprüfung nach dem Mergen. |
| 2026-10-30 | US-09 (bearbeiten oder absagen) umsetzen; Lösungsdokument nachführen. |
| 2026-11-01 | Sprint-2-Board einbinden; Gesamtprüfung vor der Abgabe (Build, Tests, Browser-Test aller Stories, Abgabe-ZIP). |
| 2026-11-01 | Funde beheben (Link im Icon-Konzept, Fehlerseiten auf Deutsch); Entwurf für Abschnitt 7 «Einsatz von KI». |
| 2026-11-02 | Dokumentation kürzen und überarbeiten. |
| 2026-11-02 | Meinen Text für Abschnitt 7 «Einsatz von KI» auf Genauigkeit prüfen und anpassen. |