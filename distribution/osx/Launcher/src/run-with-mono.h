@import Foundation;

@interface RunWithMono : NSObject {
}

+ (int) runAssemblyWithMono:(NSString *)appName procnamesuffix:(NSString *)procnamesuffix assembly:(NSString *)assembly major:(int) major minor:(int) minor;

@end