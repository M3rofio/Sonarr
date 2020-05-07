#import "run-with-mono.h"

NSString * const ASSEMBLY = @"Sonarr.exe";
NSString * const APP_NAME = @"Sonarr";
NSString * const PROCESS_NAME = @"-sonarr";
int const MONO_VERSION_MAJOR = 5;
int const MONO_VERSION_MINOR = 20;

int main() {
	@autoreleasepool {
		return [RunWithMono runAssemblyWithMono:APP_NAME procnamesuffix:PROCESS_NAME assembly:ASSEMBLY major:MONO_VERSION_MAJOR minor:MONO_VERSION_MINOR];
	}
}

