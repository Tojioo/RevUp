# Konzept: Car-Meet App

*Dokument 3 von 12 · [← 02 KI-Protokoll](02-KI-PROTOKOLL.md) · Weiter: [04 Design-Grundlagen →](Design/04-DESIGN.md)*

Ursprüngliche Idee vor dem MVP. Was davon umgesetzt ist, steht im [Product Backlog](01-BACKLOG.md).

## Übersicht

Eine App, mit der Nutzer Car-Meets organisieren und daran teilnehmen. Kernidee: Der Standort wird erst nach Angabe von Personalien freigegeben, um Störenfriede und Lärm einzudämmen. Dazu kommt eine Polizei-Integration zum Melden und Überwachen von Meets.

## Rollen

- **Teilnehmer:** meldet sich an und erhält je nach Meet direkt oder nach Freigabe den Standort.
- **Veranstalter (privat):** einzelne Person, die ein Meet organisiert.
- **Veranstalter (gewerblich):** professioneller Anbieter, evtl. mit erweiterten Funktionen (Bewilligungen, grössere Meets).
- **Polizei:** sieht alle Meets in der Umgebung und kann Warnungen oder Räumungen ankündigen.

## Kernfunktionen

- Meet mit Titel, Thema (z. B. 90s Cars, Motorräder, Green, Tuning), Zeitraum und Ort erstellen.
- **Öffentlich** (Standort direkt nach Anmeldung) oder **Privat** (Veranstalter nimmt Teilnehmer einzeln an; Standort erst nach Zusage).
- Zeitmodi **Fixed** (feste Start- und Endzeit), **Open Start** (Start ab Zeitpunkt X) und **Open End** (kein festes Ende).
- Limits für Lautstärke und Teilnehmerzahl.
- Meldefunktion: Veranstalter markiert auffällige Teilnehmer, sichtbar am Standort.
- Polizei-Integration: Meet offiziell anmelden, Übersicht für die Polizei, Warnung oder Räumungsankündigung bei Lärmbeschwerden.

## Kritische Punkte und offene Fragen

- **Datenschutz der Personalien:** hochsensible Daten. Offen ist, wer sie wie lange speichert und wer Zugriff hat (DSG). Ohne echte Prüfung sind die Angaben zudem leicht zu fälschen.
- **Missbrauch der Meldefunktion:** subjektiv und anfällig für Falschmeldungen oder Mobbing; braucht Widerspruch und Moderation, bevor eine Markierung sichtbar wird.
- **Haftung:** Bei Vorfällen am Meet ist unklar, wer haftet (Betreiber, Veranstalter, Teilnehmer). Früh mit Nutzungsbedingungen klären.
- **Polizei-Kooperation:** kein reines App-Feature, sondern ein organisatorisches Projekt mit den Behörden (Zuständigkeiten, echte Polizeikonten).
- **Open Start:** Wann der Standort für wen sichtbar wird, muss serverseitig gesteuert werden, nicht über die Uhr des Geräts.
- **Monetarisierung:** noch kein Geschäftsmodell (Abo für gewerbliche Veranstalter, Provisionen, Werbung?).
- **Datensicherheit:** Personalien plus Bewegungsmuster sind ein attraktives Ziel; Verschlüsselung, Zugriffsprotokoll und Datensparsamkeit sind Pflicht.

## Empfehlungen

- Datenschutz und Haftung rechtlich klären, bevor das technische Design vertieft wird.
- Prüfen, ob die Personalien verifiziert werden müssen, damit der Schutzzweck erfüllt ist.
- Moderationsprozess für Meldungen festlegen.
- Polizei-Funktionen als spätere, eigene Phase behandeln.
- MVP: Meet erstellen und finden, öffentlich/privat, Zeitmodi, Standortfreigabe.