```mermaid
graph LR

A(Start)

A --> B[Look for an item]

B --> C{Did you find it?}
C -->|Yes| D(Stop looking)
C -->|No| E{Ich bin dein Vater}
E -->|Yes| B
E -->|No| D
