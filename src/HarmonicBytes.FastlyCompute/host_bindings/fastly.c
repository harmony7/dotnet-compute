#include "fastly.h"

// Module fastly_http_req

int req_new(
    uint32_t *pRequestHandle
) {
    return __wasm_import_fastly_http_req_new(
        pRequestHandle
    );
}

int req_body_downstream_get(
    uint32_t *pRequestHandle,
    uint32_t *pBodyHandle
) {
    return __wasm_import_fastly_http_req_body_downstream_get(
        pRequestHandle,
        pBodyHandle
    );
}

int req_send(
    uint32_t reqHandle,
    uint32_t bodyHandle,
    const uint8_t *pBackend,
    size_t backendLen,
    uint32_t *pResponseHandle,
    uint32_t *pResponseBodyHandle
) {
    return __wasm_import_fastly_http_req_send(
        reqHandle,
        bodyHandle,
        pBackend,
        backendLen,
        pResponseHandle,
        pResponseBodyHandle
    );
}

int req_method_get(
    uint32_t requestHandle,
    uint8_t *pMethod,
    size_t methodMaxLen,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_method_get(
        requestHandle,
        pMethod,
        methodMaxLen,
        pBytesWritten
    );
}

int req_method_set(
    uint32_t requestHandle,
    uint8_t *pMethod,
    size_t methodLen
) {
    return __wasm_import_fastly_http_req_method_set(
        requestHandle,
        pMethod,
        methodLen
    );
}

int req_uri_get(
    uint32_t requestHandle,
    uint8_t *pUri,
    size_t uriMaxLen,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_uri_get(
        requestHandle,
        pUri,
        uriMaxLen,
        pBytesWritten
    );
}

int req_uri_set(
    uint32_t requestHandle,
    uint8_t *pUri,
    size_t uriLen
) {
    return __wasm_import_fastly_http_req_uri_set(
        requestHandle,
        pUri,
        uriLen
    );
}

int req_version_get(
    uint32_t requestHandle,
    uint32_t *pHttpVersion
) {
    return __wasm_import_fastly_http_req_version_get(
        requestHandle,
        pHttpVersion
    );
}

int req_version_set(
    uint32_t requestHandle,
    int32_t version
) {
    return __wasm_import_fastly_http_req_version_set(
        requestHandle,
        version
    );
}

int req_header_names_get(
    uint32_t requestHandle,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
) {
    return __wasm_import_fastly_http_req_header_names_get(
        requestHandle,
        pBuffer,
        bufferLength,
        cursor,
        pEndingCursor,
        pLength
    );
}

int req_header_values_get(
    uint32_t requestHandle,
    uint8_t *pName,
    size_t nameLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
) {
    return __wasm_import_fastly_http_req_header_values_get(
        requestHandle,
        pName,
        nameLength,
        pBuffer,
        bufferLength,
        cursor,
        pEndingCursor,
        pLength
    );
}

int req_header_insert(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
) {
    return __wasm_import_fastly_http_req_header_insert(
        requestHandle,
        pName,
        nameLength,
        pValue,
        valueLength
    );
}

int req_header_append(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
) {
    return __wasm_import_fastly_http_req_header_append(
        requestHandle,
        pName,
        nameLength,
        pValue,
        valueLength
    );    
}

int req_header_remove(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength
) {
    return __wasm_import_fastly_http_req_header_remove(
        requestHandle,
        pName,
        nameLength
    );
}

int req_redirect_to_grip_proxy(
    const uint8_t *pBackendName,
    size_t backendNameLength
) {
    return __wasm_import_fastly_http_req_redirect_to_grip_proxy(
        pBackendName,
        backendNameLength
    );
}

int req_downstream_client_ip_addr_get(
    uint8_t *pOctets,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_client_ip_addr_get(
        pOctets,
        pBytesWritten
    );
}

int req_downstream_tls_ja3_md5(
    uint8_t *pOctets,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_tls_ja3_md5(
        pOctets,
        pBytesWritten
    );
}

int req_downstream_tls_cipher_openssl_name(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_tls_cipher_openssl_name(
        pOut,
        bufferSize,
        pBytesWritten
    );
}

int req_downstream_tls_protocol(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_tls_protocol(
        pOut,
        bufferSize,
        pBytesWritten
    );
}

int req_downstream_tls_raw_client_certificate(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_tls_raw_client_certificate(
        pOut,
        bufferSize,
        pBytesWritten
    );
}

int req_downstream_tls_client_hello(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_req_downstream_tls_client_hello(
        pOut,
        bufferSize,
        pBytesWritten
    );
}

int req_cache_override_v2_set(
    uint32_t requestHandle,
    uint32_t tag,
    uint32_t ttl,
    uint32_t staleWhileRevalidate,
    const uint8_t *pSurrogateKey,
    size_t surrogateKeyLen
) {
    return __wasm_import_fastly_http_req_cache_override_v2_set(
        requestHandle,
        tag,
        ttl,
        staleWhileRevalidate,
        pSurrogateKey,
        surrogateKeyLen
    );
}

// Module fastly_http_resp

int resp_new(
    uint32_t *pResponseHandle
) {
    return __wasm_import_fastly_http_resp_new(
        pResponseHandle
    );
}

int resp_send_downstream(
    uint32_t responseHandle,
    uint32_t bodyHandle,
    uint32_t streaming
) {
    return __wasm_import_fastly_http_resp_send_downstream(
        responseHandle,
        bodyHandle,
        streaming
    );
}

int resp_status_get(
    uint32_t responseHandle,
    uint16_t *pStatus
) {
    return __wasm_import_fastly_http_resp_status_get(
        responseHandle,
        pStatus
    );
}

int resp_status_set(
    uint32_t responseHandle,
    uint16_t status
) {
    return __wasm_import_fastly_http_resp_status_set(
        responseHandle,
        status
    );
}

int resp_version_get(
    uint32_t responseHandle,
    int32_t *pVersion
) {
    return __wasm_import_fastly_http_resp_version_get(
        responseHandle,
        pVersion
    );
}

int resp_version_set(
    uint32_t responseHandle,
    int32_t version
) {
    return __wasm_import_fastly_http_resp_version_set(
        responseHandle,
        version
    );
}

int resp_header_names_get(
    uint32_t responseHandle,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
) {
    return __wasm_import_fastly_http_resp_header_names_get(
        responseHandle,
        pBuffer,
        bufferLength,
        cursor,
        pEndingCursor,
        pLength
    );
}

int resp_header_values_get(
    uint32_t responseHandle,
    uint8_t *pName,
    size_t nameLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
) {
    return __wasm_import_fastly_http_resp_header_values_get(
        responseHandle,
        pName,
        nameLength,
        pBuffer,
        bufferLength,
        cursor,
        pEndingCursor,
        pLength
    );
}

int resp_header_insert(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
) {
    return __wasm_import_fastly_http_resp_header_insert(
        responseHandle,
        pName,
        nameLength,
        pValue,
        valueLength
    );
}

int resp_header_append(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
) {
    return __wasm_import_fastly_http_resp_header_append(
        responseHandle,
        pName,
        nameLength,
        pValue,
        valueLength
    );    
}

int resp_header_remove(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength
) {
    return __wasm_import_fastly_http_resp_header_remove(
        responseHandle,
        pName,
        nameLength
    );
}

// Module fastly_http_body

int body_new(
    uint32_t *pBodyHandle
) {
    return __wasm_import_fastly_http_body_new(
        pBodyHandle
    );
}

int body_read(
    uint32_t bodyHandle,
    uint8_t *pBuffer,
    size_t bufferSize,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_body_read(
        bodyHandle,
        pBuffer,
        bufferSize,
        pBytesWritten
    );
}

int body_write(
    uint32_t bodyHandle,
    uint8_t *pBuffer,
    size_t bufferSize,
    uint32_t bodyWriteEnd,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_http_body_write(
        bodyHandle,
        pBuffer,
        bufferSize,
        bodyWriteEnd,
        pBytesWritten
    );
}

int body_close(
    uint32_t bodyHandle
) {
    return __wasm_import_fastly_http_body_close(
        bodyHandle
    );
}

// Module fastly_geo

int geo_lookup(
    const uint8_t *pAddressOctets,
    size_t addressOctetsLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_geo_lookup(
        pAddressOctets,
        addressOctetsLength,
        pBuffer,
        bufferLength,
        pBytesWritten
    );
}

// Module fastly_dictionary

int dictionary_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pDictionaryHandle
) {
    return __wasm_import_fastly_dictionary_open(
        pName,
        nameLength,
        pDictionaryHandle
    );
}

int dictionary_get(
    uint32_t dictionaryHandle,
    uint8_t *key,
    size_t keyLength,
    uint8_t *pValue,
    size_t valueMaxLength,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_dictionary_get(
        dictionaryHandle,
        key,
        keyLength,
        pValue,
        valueMaxLength,
        pBytesWritten
    );
}

// Module fastly_secret_store

int secret_store_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pSecretStoreHandle
) {
    return __wasm_import_fastly_secret_store_open(
        pName,
        nameLength,
        pSecretStoreHandle
    );
}

int secret_store_get(
    uint32_t secretStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t *pSecretStoreSecretHandle
) {
    return __wasm_import_fastly_secret_store_get(
        secretStoreHandle,
        pKey,
        keyLength,
        pSecretStoreSecretHandle
    );
}

int secret_store_plaintext(
    uint32_t secretStoreSecretHandle,
    uint8_t *pValue,
    size_t valueMaxLength,
    size_t *pBytesWritten
) {
    return __wasm_import_fastly_secret_store_plaintext(
        secretStoreSecretHandle,
        pValue,
        valueMaxLength,
        pBytesWritten
    );
}

// Module fastly_object_store

int object_store_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pObjectStoreHandle
) {
    return __wasm_import_fastly_object_store_open(
        pName,
        nameLength,
        pObjectStoreHandle
    );
}

int object_store_lookup(
    uint32_t objectStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t *pBodyHandle
) {
    return __wasm_import_fastly_object_store_lookup(
        objectStoreHandle,
        pKey,
        keyLength,
        pBodyHandle
    );
}

int object_store_insert(
    uint32_t objectStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t bodyHandle
) {
    return __wasm_import_fastly_object_store_insert(
        objectStoreHandle,
        pKey,
        keyLength,
        bodyHandle
    );
}

// Module fastly_purge

int purge_surrogate_key(
    uint8_t *pSurrogateKey,
    size_t surrogateKeyLength,
    uint32_t optionsMask,
    PurgeOptions *pPurgeOptions
) {
    return __wasm_import_fastly_purge_surrogate_key(
        pSurrogateKey,
        surrogateKeyLength,
        optionsMask,
        pPurgeOptions
    );
}
