#!/bin/bash

set -e

argument=$1

if [ "$argument" == "fix" ]; then
    npx mega-linter-runner --flavor dotnetweb -e 'APPLY_FIXES=all'
else
    npx mega-linter-runner --flavor dotnetweb
fi
