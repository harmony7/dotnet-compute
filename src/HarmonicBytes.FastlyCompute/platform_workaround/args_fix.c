// This is a hack that works with Mono's entrypoint
// The call is injected using HarmonicBytes.FastlyCompute.targets

char *ARGS[] = { "compute" };
void _args_fix(int *argc, char **argv[])
{
    if (*argc == 0)
    {
        *argv = ARGS;
        *argc = 1;
    }
}
