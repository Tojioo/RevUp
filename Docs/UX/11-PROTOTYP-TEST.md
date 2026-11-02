# 6. Prototyp testen

*Dokument 11 von 12 · [← 10 Wireframes](10-wireframes.html) · Weiter: [Prototyp (Hi-Fi) →](Prototype/README.md) · [12 Lösungsdokument →](../12-LOESUNGSDOKUMENT.md)*

**Methode:** Selbsttest (Cognitive Walkthrough) anhand der [Wireframes](10-wireframes.html), ohne externe Testpersonen. Grenze: Als Entwickler ist man kein neutraler Testnutzer. Für den MVP reicht das.

## Aufgaben und Klickpfade

| Aufgabe                                | Pfad                                                                       | Schritte              |
|----------------------------------------|----------------------------------------------------------------------------|-----------------------|
| Öffentliches Meet finden und beitreten | Entdecken → Thema filtern → Meet öffnen → Name eingeben, beitreten         | 3 Klicks + 1 Eingabe  |
| Meet erstellen                         | Erstellen → Formular ausfüllen → Absenden                                  | 2 Klicks + 7 Eingaben |
| Private Anfrage verwalten              | Meine Meets → Annehmen/Ablehnen                                            | 2 Klicks              |

Alle drei Aufgaben lassen sich ohne Sackgassen durchspielen. Das Erstellen-Formular hat mit 7 Feldern die höchste Eingabelast, passt aber zum Konzept (die Rahmenbedingungen sind der Kern).

## Gefundene Probleme

- **Ein Button für zwei Fälle:** Die Detailseite zeigt nur «Anfrage stellen». Bei öffentlichen Meets muss er «Beitreten» heissen und sofort den Standort zeigen. **Folge:** Text und Verhalten hängen an der Sichtbarkeit (US-04 vs. US-05).
- **Keine Validierungshinweise:** Bei 7 Pflichtfeldern merkt man Fehler erst nach dem Absenden. **Folge:** Fehlermeldungen direkt am Feld, serverseitig geprüft.
- **Kein Leerzustand:** Unklar, was die Liste zeigt, wenn kein Meet zum Filter passt. **Folge:** eigener Text für den Leerzustand.

Alle drei Punkte sind in die Umsetzung eingeflossen; die Wireframes mussten nicht zurückgebaut werden.