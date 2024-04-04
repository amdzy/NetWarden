
TUI_PROJECT_NAME := NetWarden.Tui
ARTIFACT_FOLDER := artifacts
OUTPUT_FOLDER := bin/Release/net8.0/publish

## help: print this help message
.PHONY: help
help:
	@echo 'Usage:'
	@sed -n 's/^##//p' ${MAKEFILE_LIST} | column -t -s ':' | sed -e 's/^/ /'

## clean: clean build artifacts
.PHONY: clean
clean:
	dotnet clean
	rm -rf $(ARTIFACT_FOLDER)

## publish/tui/all: Publish the tui app for all archtype
.PHONY: publish/tui/all
publish/tui/all: publish/tui/linux/x64 publish/tui/osx/x64 publish/tui/win/x64

## publish/tui/linux/x64: Publish the tui app for linux-x64
.PHONY: publish/tui/linux/x64
publish/tui/linux/x64:
	dotnet publish -c Release -r linux-x64 -o ${ARTIFACT_FOLDER}  $(TUI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Tui ${ARTIFACT_FOLDER}/netwarden
	tar czf ${ARTIFACT_FOLDER}/NetWarden_linux_x64.tar.gz -C ${ARTIFACT_FOLDER} netwarden
	rm ${ARTIFACT_FOLDER}/netwarden

## publish/tui/osx/x64: Publish the tui app for osx-x64
.PHONY: publish/tui/osx/x64
publish/tui/osx/x64:
	dotnet publish -c Release -r osx-x64 -o ${ARTIFACT_FOLDER}  $(TUI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Tui ${ARTIFACT_FOLDER}/netwarden
	tar czf ${ARTIFACT_FOLDER}/NetWarden_osx_x64.tar.gz -C ${ARTIFACT_FOLDER} netwarden
	rm ${ARTIFACT_FOLDER}/netwarden

## publish/tui/win/x64: Publish the tui app for win-x64
.PHONY: publish/tui/win/x64
publish/tui/win/x64:
	dotnet publish -c Release -r win-x64 -o ${ARTIFACT_FOLDER}  $(TUI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Tui.exe ${ARTIFACT_FOLDER}/NetWarden.exe
	zip -j ${ARTIFACT_FOLDER}/NetWarden_win_x64.zip ${ARTIFACT_FOLDER}/NetWarden.exe
	rm ${ARTIFACT_FOLDER}/NetWarden.exe