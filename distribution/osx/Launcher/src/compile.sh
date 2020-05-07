#!/bin/bash
# -fobjc-arc: enables ARC
# -fmodules: enables modules so you can import with `@import AppKit;`
# -mmacosx-version-min=10.6: support older OS X versions, this might increase the binary size

if [ ! -d "../bin" ]; then mkdir ../bin; fi

clang run-with-mono.m Sonarr.m -fobjc-arc -fmodules -mmacosx-version-min=10.6 -o ../bin/Sonarr
clang run-with-mono.m Sonarr.Update.m -fobjc-arc -fmodules -mmacosx-version-min=10.6 -o ../bin/Sonarr.Update
