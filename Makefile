
CLI_PROJECT_NAME := NetWarden.Cli
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

## publish/cli/all: Publish the cli app for all archtype
.PHONY: publish/cli/all
publish/cli/all: publish/cli/linux/x64 publish/cli/osx/x64 publish/cli/win/x64

## publish/cli/linux/x64: Publish the cli app for linux-x64
.PHONY: publish/cli/linux/x64
publish/cli/linux/x64:
	dotnet publish -c Release -r linux-x64 -o ${ARTIFACT_FOLDER}  $(CLI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Cli ${ARTIFACT_FOLDER}/netwarden
	tar czf ${ARTIFACT_FOLDER}/NetWarden_linux_x64.tar.gz -C ${ARTIFACT_FOLDER} netwarden
	rm ${ARTIFACT_FOLDER}/netwarden

## publish/cli/osx/x64: Publish the cli app for osx-x64
.PHONY: publish/cli/osx/x64
publish/cli/osx/x64:
	dotnet publish -c Release -r osx-x64 -o ${ARTIFACT_FOLDER}  $(CLI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Cli ${ARTIFACT_FOLDER}/netwarden
	tar czf ${ARTIFACT_FOLDER}/NetWarden_osx_x64.tar.gz -C ${ARTIFACT_FOLDER} netwarden
	rm ${ARTIFACT_FOLDER}/netwarden

## publish/cli/win/x64: Publish the cli app for win-x64
.PHONY: publish/cli/win/x64
publish/cli/win/x64:
	dotnet publish -c Release -r win-x64 -o ${ARTIFACT_FOLDER}  $(CLI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetWarden.Cli.exe ${ARTIFACT_FOLDER}/NetWarden.exe
	zip -j ${ARTIFACT_FOLDER}/NetWarden_win_x64.zip ${ARTIFACT_FOLDER}/NetWarden.exe
	rm ${ARTIFACT_FOLDER}/NetWarden.exe