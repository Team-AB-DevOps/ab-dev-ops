
all: fix


fix:
	npx mega-linter-runner --flavor dotnetweb -e 'APPLY_FIXES=all'

lint:
	npx mega-linter-runner --flavor dotnetweb
