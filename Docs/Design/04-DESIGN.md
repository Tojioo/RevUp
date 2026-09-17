# RevUp: Design Basics

*Dokument 4 von 11 · [← 03 Konzept](../03-KONZEPT.md) · Weiter: [05 Icon-Konzept →](05-gauge-mark.html)*

This is a loose guideline, not a hard rule. If a color combination does not work in practice, adjust or rework it.

## Color Philosophy

The palette is built on a dark, automotive-inspired base tone with a single strong accent (mint/teal) that carries action, "active/live" status, and brand identity. All other colors derive from a few base values (tint/shade/opacity) so the palette stays consistent instead of growing arbitrarily.

## Base Colors (Brand & Surface)

| Role                      | Hex                      | Usage                                            |
|---------------------------|--------------------------|--------------------------------------------------|
| Background (base surface) | `#0C1311`                | App background, deepest layer                    |
| Surface 1                 | `#112320`                | Cards, panels (first elevation)                  |
| Surface 2                 | `#1D424A`                | Elevated elements, input fields, navbar          |
| Border/divider            | `#17302D`                | Borders, dividers                                |
| Text (primary)            | `#CBD9D7`                | Body text on dark background                     |
| Accent (brand)            | `#00DBAC`                | Primary actions, links, active states            |
| Accent, muted             | `#00DBAC` at 12% opacity | Hover/selection backgrounds, subtle highlighting |

All lighter surface tones come from lightening the background, not from grayscale values. This keeps the greenish base mood intact.

## Secondary Colors

The accent `#00DBAC` sits at roughly 166° on the color wheel. The secondary colors are split-complementary to it (rather than directly complementary, to avoid colliding with the error color) and are deliberately desaturated. They give good contrast on dark backgrounds without a harsh effect on extended viewing.

| Role                              | Hex       | Usage                                                                                               |
|-----------------------------------|-----------|-----------------------------------------------------------------------------------------------------|
| Secondary, warm (copper/sand)     | `#D9985C` | Secondary CTAs, highlights that should stand apart from the brand accent without implying "success" |
| Secondary, cool (muted blue-gray) | `#7A93A8` | Structural/tertiary elements, calm highlighting, metadata accents                                   |

Both reach a contrast ratio of at least 4.5:1 for text on `#0C1311` and `#112320`. Their reduced saturation (roughly 45 to 55%) keeps them from feeling tiring even in large areas or frequent repetition.

## Semantic Colors

Functional states get their own colors, clearly distinct from the brand accent. This matters because RevUp shows safety- and status-relevant messages (warnings, clearance announcements, approvals).

| State                                             | Hex                        | Rationale                                                                  |
|---------------------------------------------------|----------------------------|----------------------------------------------------------------------------|
| Success / approved                                | `#00DBAC` (= brand accent) | Green reads universally as "positive/clear"; no extra color needed         |
| Warning                                           | `#FFB020`                  | Warm contrast to green, standard signal color for caution                  |
| Error / critical (e.g. police warning, clearance) | `#FF5470`                  | Clear red tone, readable on dark backgrounds, no confusion with the accent |
| Information                                       | `#4DA3FF`                  | Neutral-informative, clearly distinguishable from success/error            |

Principle: every semantic color must reach a contrast ratio of at least 4.5:1 for text and 3:1 for graphical elements against `#0C1311` and `#112320` (WCAG AA).

## Derivation Rules

- **Hover state:** lighten the base color by roughly 8 to 10% on dark surfaces, or darken it by the same amount on light/accent surfaces.
- **Disabled state:** reduce the color to 40 to 50% opacity. Never introduce a separate grayscale.
- **Muted backgrounds** (e.g. badges, subtle fills): base color at 10 to 15% opacity on the surface it sits on.
- **Text on colored surfaces:** never pure white or black. Always use the defined text tone or background tone to preserve the overall character.

## Typography (general)

- A clean, legible sans-serif for UI text. A second, more distinctive typeface is optional, reserved for headlines/brand moments, used sparingly.
- Clear hierarchy (size and/or weight) between title, subtitle, body text, and metadata. Keep the number of steps small on purpose.
- Choose line length and line height so body text on a dark background stays comfortable to read. A dark background is less forgiving than a light one.

## Spacing & Structure (general)

- A consistent spacing scale (e.g. multiples of a base unit) instead of arbitrary pixel values.
- Elevation/hierarchy primarily through surface color (Surface 1 vs. Surface 2) rather than heavy shadows. This fits the dark, flat base character.
- Where shadows are needed: use a muted, colored tone (accent color, very low opacity) instead of neutral black, for consistency with the overall mood.

## Interaction & States (general)

- Active/selected elements stand out primarily through the brand accent (border, text or icon color, muted fill).
- Icons follow the text color unless they carry their own semantic meaning (warning, error, etc.).
- Keep motion and transitions short and subtle. They should support state changes, not decorate them.
- Reserve the brand accent for interactive elements (buttons, active states, links). A purely informational category label (e.g. a "public" tag on a list item) should use a different tone (info blue, muted surface) so the accent keeps one unambiguous meaning: "you can act here."

## Accessibility

- Check contrast ratios against WCAG AA for every text/background combination, especially the accent and error colors on dark backgrounds.
- Never communicate states (success, warning, error) through color alone. Always pair them with an icon or text.
- Make interactive elements large enough and give them a visible focus state (e.g. the accent color as a focus ring).