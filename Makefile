
CLI_PROJECT_NAME := NetManager.Cli
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
	mv ${ARTIFACT_FOLDER}/NetManager.Cli ${ARTIFACT_FOLDER}/netManager
	tar czf ${ARTIFACT_FOLDER}/NetManager_linux_x64.tar.gz -C ${ARTIFACT_FOLDER} netManager
	rm ${ARTIFACT_FOLDER}/netManager

## publish/cli/osx/x64: Publish the cli app for osx-x64
.PHONY: publish/cli/osx/x64
publish/cli/osx/x64:
	dotnet publish -c Release -r osx-x64 -o ${ARTIFACT_FOLDER}  $(CLI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetManager.Cli ${ARTIFACT_FOLDER}/netManager
	tar czf ${ARTIFACT_FOLDER}/NetManager_osx_x64.tar.gz -C ${ARTIFACT_FOLDER} netManager
	rm ${ARTIFACT_FOLDER}/netManager

## publish/cli/win/x64: Publish the cli app for win-x64
.PHONY: publish/cli/win/x64
publish/cli/win/x64:
	dotnet publish -c Release -r win-x64 -o ${ARTIFACT_FOLDER}  $(CLI_PROJECT_NAME)
	mv ${ARTIFACT_FOLDER}/NetManager.Cli.exe ${ARTIFACT_FOLDER}/netManager.exe
	zip -j ${ARTIFACT_FOLDER}/NetManager_win_x64.zip ${ARTIFACT_FOLDER}/netManager.exe
	rm ${ARTIFACT_FOLDER}/netManager.exe