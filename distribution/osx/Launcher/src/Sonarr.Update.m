#import "run-with-mono.h"

NSString * const ASSEMBLY = @"Sonarr.Update.exe";
NSString * const APP_NAME = @"Sonarr Updater";
NSString * const PROCESS_NAME = @"-sonarrupdate";
int const MONO_VERSION_MAJOR = 5;
int const MONO_VERSION_MINOR = 20;

int main() {
	@autoreleasepool {
		return [RunWithMono runAssemblyWithMono:APP_NAME procnamesuffix:PROCESS_NAME assembly:ASSEMBLY major:MONO_VERSION_MAJOR minor:MONO_VERSION_MINOR];
	}
}

