# Claritty
A terminal emulator that renders an OpenGL canvas that programs may write anything to, making any visuals possible

Currently a bare minimum proof-of-concept

Simply renders the contents of `/dev/shm/ClarittyGraphicsLower` below the text, and `/dev/shm/ClarittyGraphicsUpper` above it. If fleshed out further, each session would have its own canvas
