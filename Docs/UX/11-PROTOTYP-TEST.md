# 6. Prototyp testen

*Dokument 11 von 11 · [← 10 Wireframes](10-wireframes.html) · Weiter: [Prototyp (Hi-Fi) →](Prototype/README.md)*

**Methode:** Leichtgewichtiger Selbsttest (Cognitive Walkthrough) anhand der Wireframes in [`10-wireframes.html`](10-wireframes.html), keine externen Testpersonen. Grenzen dieser Methode: als Entwickler ist man nie ein neutraler Testnutzer, blinde Flecken bleiben wahrscheinlich unentdeckt. Für den MVP-Rahmen ausreichend, siehe Abwägung im Chat-Verlauf zu diesem Dokument.

## Aufgaben und Effizienz (Klickpfade)

| Aufgabe                                | Pfad                                                                       | Schritte              |
|----------------------------------------|----------------------------------------------------------------------------|-----------------------|
| Öffentliches Meet finden und beitreten | Entdecken (Start) → Thema filtern → Meet öffnen → Name eingeben, beitreten | 3 Klicks + 1 Eingabe  |
| Meet erstellen                         | Erstellen → Formular ausfüllen → Absenden                                  | 2 Klicks + 7 Eingaben |
| Private Anfrage verwalten              | Meine Meets → Annehmen/Ablehnen                                            | 2 Klicks              |

Die ersten beiden Aufgaben sind kurz genug (2 bis 3 Klicks). Das Erstellen-Formular hat mit 7 Feldern die höchste Eingabelast im MVP.

## Gefundene Probleme

- **Detail-Ansicht unterscheidet Button-Text nicht:** Im Wireframe zeigt die Detailseite nur "Anfrage stellen" (privates Beispiel). Bei öffentlichen Meets muss der Button stattdessen "Beitreten" heissen und sofort den Standort zeigen, nicht erst eine Anfrage auslösen. Das war im ersten Entwurf nicht klar genug getrennt. **Konsequenz:** Button-Text und -Verhalten müssen explizit an `IsPublic` hängen, nicht als Variante desselben Buttons behandelt werden (Story 4 vs. 5).
- **Formular ohne sichtbare Validierungshinweise:** 7 Pflichtfelder ohne erkennbares Feedback, wo ein Fehler liegt. Risiko: Nutzer merkt Probleme erst nach dem Absenden. **Konsequenz:** Inline-Validierung direkt am Feld einplanen (Backlog, nicht-funktionale Anforderungen, bereits vermerkt: "serverseitig validiert").
- **Kein Leerzustand vorgesehen:** Weder Wireframe noch Backlog behandeln, was die Entdecken-Liste zeigt, wenn kein Meet zum gewählten Filter passt. **Konsequenz:** Leerzustand ("Keine Meets zu diesem Thema") als kleine zusätzliche Story/Detail vormerken.

## Grobe Einschätzung (Effektivität / Zufriedenheit)

- **Effektivität:** Alle drei Kernaufgaben lassen sich anhand der Wireframes ohne Sackgassen durchspielen.
- **Zufriedenheit:** Struktur wirkt aufgeräumt, das Erstellen-Formular fühlt sich beim Durchgehen länger an als die anderen Aufgaben. Passt aber zum Funktionsumfang (viele Rahmenbedingungen sind Kern des Konzepts, siehe `03-KONZEPT.md`), keine Kürzung vorgesehen.

## Ergebnis

Die drei gefundenen Punkte fliessen als Klarstellung in die Umsetzung von Story 4/5 (Button-Verhalten) und die nicht-funktionalen Anforderungen (Validierung, Leerzustand) ein, kein Rückbau der Wireframes nötig.