# RevUp: Design-Grundlagen

*Dokument 4 von 12 · [← 03 Konzept](../03-KONZEPT.md) · Weiter: [05 Icon-Konzept →](05-gauge-mark.html)*

Lose Richtlinie, keine feste Regel: Funktioniert eine Kombination in der Praxis nicht, wird sie angepasst.

## Farben

Dunkler, an Autos angelehnter Grundton mit einem kräftigen Akzent (Mint) für Aktionen, «aktiv/live» und die Marke. Alle weiteren Töne entstehen aus wenigen Grundwerten (aufhellen, abdunkeln, Deckkraft), damit die Palette nicht beliebig wächst.

| Rolle | Hex | Verwendung |
|---|---|---|
| Hintergrund | `#0C1311` | App-Hintergrund, tiefste Ebene |
| Fläche 1 | `#112320` | Karten, Panels |
| Fläche 2 | `#1D424A` | Erhöhte Elemente, Eingabefelder, Navigation |
| Rahmen | `#17302D` | Rahmen, Trennlinien |
| Text | `#CBD9D7` | Fliesstext auf dunklem Grund |
| Akzent | `#00DBAC` | Hauptaktionen, Links, aktive Zustände |
| Akzent gedämpft | `#00DBAC` mit 12 % Deckkraft | Hover- und Auswahlhintergründe |

Hellere Flächen entstehen durch Aufhellen des Hintergrunds, nicht aus Grautönen, damit der grünliche Grundton bleibt.

**Sekundärfarben:** split-komplementär zum Akzent und bewusst entsättigt; Kontrast mindestens 4.5:1 auf `#0C1311` und `#112320`.

| Rolle | Hex | Verwendung |
|---|---|---|
| Warm (Kupfer/Sand) | `#D9985C` | Sekundäre Aktionen und Hervorhebungen, die nicht «Erfolg» bedeuten sollen |
| Kühl (Blaugrau) | `#7A93A8` | Strukturelle Elemente, ruhige Hervorhebung, Metadaten |

**Zustandsfarben:** klar vom Akzent getrennt, weil RevUp sicherheitsrelevante Meldungen zeigt; Kontrast mindestens 4.5:1 für Text und 3:1 für Grafik (WCAG AA).

| Zustand | Hex | Begründung |
|---|---|---|
| Erfolg / freigegeben | `#00DBAC` (= Akzent) | Grün wird allgemein als «positiv» gelesen |
| Warnung | `#FFB020` | Warmer Kontrast zu Grün, übliche Warnfarbe |
| Fehler / kritisch | `#FF5470` | Klares Rot, gut lesbar, keine Verwechslung mit dem Akzent |
| Information | `#4DA3FF` | Neutral, klar von Erfolg und Fehler unterscheidbar |

## Ableitungsregeln

- Hover: auf dunklen Flächen um etwa 8–10 % aufhellen, auf hellen gleich viel abdunkeln.
- Deaktiviert: 40–50 % Deckkraft, keine eigene Graustufe.
- Gedämpfte Hintergründe (Badges, Füllungen): Grundfarbe mit 10–15 % Deckkraft.
- Text auf Farbflächen nie reines Weiss oder Schwarz, sondern im Text- oder Hintergrundton.

## Typografie, Abstände, Interaktion

- Eine gut lesbare serifenlose Schrift für die Oberfläche, optional eine markantere für Titel; wenige, klare Hierarchiestufen.
- Abstände nach einer festen Skala. Ebenen über die Flächenfarbe statt über starke Schatten; nötige Schatten leicht in Akzentfarbe statt Schwarz.
- Der Akzent ist für Interaktives reserviert (Buttons, aktive Zustände, Links). Reine Kategorie-Labels wie «Öffentlich» nutzen einen anderen Ton (Info-Blau), damit Mint eindeutig «hier kannst du handeln» bedeutet.
- Icons folgen der Textfarbe, ausser sie tragen eine eigene Bedeutung (Warnung, Fehler). Animationen kurz und dezent.

## Barrierefreiheit

- Kontrast jeder Text-/Hintergrund-Kombination nach WCAG AA prüfen.
- Zustände nie nur über Farbe zeigen, sondern immer mit Icon oder Text.
- Interaktive Elemente gross genug und mit sichtbarem Fokus (z. B. Akzentfarbe als Fokusring).