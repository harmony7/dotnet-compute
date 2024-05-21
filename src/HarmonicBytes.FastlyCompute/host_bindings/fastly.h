#ifndef FASTLY_H
#define FASTLY_H
#ifdef __cplusplus
extern "C" {
#endif

#include <stdbool.h>
#include <stddef.h>
#include <stdint.h>

// Module fastly_http_req

__attribute__((import_module("fastly_http_req"), import_name("new")))
int __wasm_import_fastly_http_req_new(
    uint32_t *pRequestHandle
);

__attribute__((import_module("fastly_http_req"), import_name("body_downstream_get")))
int __wasm_import_fastly_http_req_body_downstream_get(
    uint32_t *pRequestHandle,
    uint32_t *pBodyHandle
);

__attribute__((import_module("fastly_http_req"), import_name("send")))
int __wasm_import_fastly_http_req_send(
    uint32_t reqHandle,
    uint32_t bodyHandle,
    const uint8_t *pBackend,
    size_t backendLen,
    uint32_t *pResponseHandle,
    uint32_t *pResponseBodyHandle
);

__attribute__((import_module("fastly_http_req"), import_name("method_get")))
int __wasm_import_fastly_http_req_method_get(
    uint32_t requestHandle,
    uint8_t *pMethod,
    size_t methodMaxLen,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("method_set")))
int __wasm_import_fastly_http_req_method_set(
    uint32_t requestHandle,
    uint8_t *pMethod,
    size_t methodLen
);

__attribute__((import_module("fastly_http_req"), import_name("uri_get")))
int __wasm_import_fastly_http_req_uri_get(
    uint32_t requestHandle,
    uint8_t *pUri,
    size_t uriMaxLen,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("uri_set")))
int __wasm_import_fastly_http_req_uri_set(
    uint32_t requestHandle,
    uint8_t *pUri,
    size_t uriLen
);

__attribute__((import_module("fastly_http_req"), import_name("version_get")))
int __wasm_import_fastly_http_req_version_get(
    uint32_t requestHandle,
    uint32_t *pHttpVersion
);

__attribute__((import_module("fastly_http_req"), import_name("version_set")))
int __wasm_import_fastly_http_req_version_set(
    uint32_t requestHandle,
    int32_t version
);

__attribute__((import_module("fastly_http_req"), import_name("header_names_get")))
int __wasm_import_fastly_http_req_header_names_get(
    uint32_t requestHandle,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
);

__attribute__((import_module("fastly_http_req"), import_name("header_values_get")))
int __wasm_import_fastly_http_req_header_values_get(
    uint32_t requestHandle,
    uint8_t *pName,
    size_t nameLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
);

__attribute__((import_module("fastly_http_req"), import_name("header_insert")))
int __wasm_import_fastly_http_req_header_insert(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
);

__attribute__((import_module("fastly_http_req"), import_name("header_append")))
int __wasm_import_fastly_http_req_header_append(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
);

__attribute__((import_module("fastly_http_req"), import_name("header_remove")))
int __wasm_import_fastly_http_req_header_remove(
    uint32_t requestHandle,
    const uint8_t *pName,
    size_t nameLength
);

__attribute__((import_module("fastly_http_req"), import_name("redirect_to_grip_proxy")))
int __wasm_import_fastly_http_req_redirect_to_grip_proxy(
    const uint8_t *pBackendName,
    size_t backendNameLength
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_client_ip_addr")))
int __wasm_import_fastly_http_req_downstream_client_ip_addr_get(
    uint8_t *pOctets,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_tls_ja3_md5")))
int __wasm_import_fastly_http_req_downstream_tls_ja3_md5(
    uint8_t *pOctets,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_tls_cipher_openssl_name")))
int __wasm_import_fastly_http_req_downstream_tls_cipher_openssl_name(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_tls_protocol")))
int __wasm_import_fastly_http_req_downstream_tls_protocol(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_tls_raw_client_certificate")))
int __wasm_import_fastly_http_req_downstream_tls_raw_client_certificate(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("downstream_tls_client_hello")))
int __wasm_import_fastly_http_req_downstream_tls_client_hello(
    uint8_t *pOut,
    size_t bufferSize,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_req"), import_name("cache_override_v2_set")))
int __wasm_import_fastly_http_req_cache_override_v2_set(
    uint32_t requestHandle,
    uint32_t tag,
    uint32_t ttl,
    uint32_t staleWhileRevalidate,
    const uint8_t *pSsurrogateKey,
    size_t surrogateKeyLen
);

// Module fastly_http_resp

__attribute__((import_module("fastly_http_resp"), import_name("new")))
int __wasm_import_fastly_http_resp_new(
    uint32_t *pResponseHandle
);

__attribute__((import_module("fastly_http_resp"), import_name("send_downstream")))
int __wasm_import_fastly_http_resp_send_downstream(
    uint32_t responseHandle,
    uint32_t bodyHandle,
    uint32_t streaming
);

__attribute__((import_module("fastly_http_resp"), import_name("status_get")))
int __wasm_import_fastly_http_resp_status_get(
    uint32_t responseHandle,
    uint16_t *pStatus
);

__attribute__((import_module("fastly_http_resp"), import_name("status_set")))
int __wasm_import_fastly_http_resp_status_set(
    uint32_t responseHandle,
    uint16_t status
);
    
__attribute__((import_module("fastly_http_resp"), import_name("version_get")))
int __wasm_import_fastly_http_resp_version_get(
    uint32_t responseHandle,
    int32_t *pVersion
);

__attribute__((import_module("fastly_http_resp"), import_name("version_set")))
int __wasm_import_fastly_http_resp_version_set(
    uint32_t responseHandle,
    int32_t version
);

__attribute__((import_module("fastly_http_resp"), import_name("header_names_get")))
int __wasm_import_fastly_http_resp_header_names_get(
    uint32_t responseHandle,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
);

__attribute__((import_module("fastly_http_resp"), import_name("header_values_get")))
int __wasm_import_fastly_http_resp_header_values_get(
    uint32_t responseHandle,
    uint8_t *pName,
    size_t nameLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    int32_t cursor,
    int32_t *pEndingCursor,
    uint32_t *pLength
);

__attribute__((import_module("fastly_http_resp"), import_name("header_insert")))
int __wasm_import_fastly_http_resp_header_insert(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
);

__attribute__((import_module("fastly_http_resp"), import_name("header_append")))
int __wasm_import_fastly_http_resp_header_append(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength,
    const uint8_t *pValue,
    size_t valueLength
);

__attribute__((import_module("fastly_http_resp"), import_name("header_remove")))
int __wasm_import_fastly_http_resp_header_remove(
    uint32_t responseHandle,
    const uint8_t *pName,
    size_t nameLength
);

// Module fastly_http_body

__attribute__((import_module("fastly_http_body"), import_name("new")))
int __wasm_import_fastly_http_body_new(
    uint32_t *pBodyHandle
);
    
__attribute__((import_module("fastly_http_body"), import_name("read")))
int __wasm_import_fastly_http_body_read(
    uint32_t bodyHandle,
    uint8_t *pBuffer,
    size_t bufferSize,
    size_t *pBytesWritten
);

__attribute__((import_module("fastly_http_body"), import_name("write")))
int __wasm_import_fastly_http_body_write(
    uint32_t bodyHandle,
    uint8_t *pBuffer,
    size_t bufferSize,
    uint32_t bodyWriteEnd,
    size_t *pBytesWritten
);
    
__attribute__((import_module("fastly_http_body"), import_name("close")))
int __wasm_import_fastly_http_body_close(
    uint32_t bodyHandle
);

// Module fastly_geo

__attribute__((import_module("fastly_geo"), import_name("lookup")))
int __wasm_import_fastly_geo_lookup(
    const uint8_t *pAddressOctets,
    size_t addressOctetsLength,
    uint8_t *pBuffer,
    size_t bufferLength,
    size_t *pBytesWritten
);

// Module fastly_dictionary

__attribute__((import_module("fastly_dictionary"), import_name("open")))
int __wasm_import_fastly_dictionary_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pDictionaryHandle
);

__attribute__((import_module("fastly_dictionary"), import_name("get")))
int __wasm_import_fastly_dictionary_get(
    uint32_t dictionaryHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint8_t *pValue,
    size_t valueMaxLength,
    size_t *pBytesWritten
);

// Module fastly_secret_store

__attribute__((import_module("fastly_secret_store"), import_name("open")))
int __wasm_import_fastly_secret_store_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pSecretStoreHandle
);

__attribute__((import_module("fastly_secret_store"), import_name("get")))
int __wasm_import_fastly_secret_store_get(
    uint32_t secretStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t *pSecretStoreSecretHandle
);

__attribute__((import_module("fastly_secret_store"), import_name("plaintext")))
int __wasm_import_fastly_secret_store_plaintext(
    uint32_t secretStoreSecretHandle,
    uint8_t *pValue,
    size_t valueMaxLength,
    size_t *pBytesWritten
);

// Module fastly_object_store

__attribute__((import_module("fastly_object_store"), import_name("open")))
int __wasm_import_fastly_object_store_open(
    uint8_t *pName,
    size_t nameLength,
    uint32_t *pObjectStoreHandle
);

__attribute__((import_module("fastly_object_store"), import_name("lookup")))
int __wasm_import_fastly_object_store_lookup(
    uint32_t objectStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t *pBodyHandle
);

__attribute__((import_module("fastly_object_store"), import_name("insert")))
int __wasm_import_fastly_object_store_insert(
    uint32_t objectStoreHandle,
    uint8_t *pKey,
    size_t keyLength,
    uint32_t bodyHandle
);

// Module fastly_purge

typedef struct __attribute__((aligned(4))) tagPurgeOptions {
    uint8_t *pBuffer;
    size_t bufferLength;
    size_t *pBytesWritten;
} PurgeOptions;

__attribute__((import_module("fastly_purge"), import_name("purge_surrogate_key")))
int __wasm_import_fastly_purge_surrogate_key(
    uint8_t *pSurrogateKey,
    size_t surrogateKeyLength,
    uint32_t optionsMask,
    PurgeOptions *pPurgeOptions
);

#ifdef __cplusplus
}
#endif
#endif // FASTLY_H
