#ifdef MYDLLH
#define MYDLLH

_declspec(dllexport) void LogInit(const char* logPath, const char* prefix, int logLevel);
_declspec(dllexport) void LogWrite(const char* logMessage, const char* logSeverity);

#endif