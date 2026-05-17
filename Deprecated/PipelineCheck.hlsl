void IsBirp_float(out bool isBirp) {
    #if defined(BUILTIN_TARGET_API) || SHADERGRAPH_PREVIEW
        isBirp = true;
    #else
        isBirp = false;
    #endif
}