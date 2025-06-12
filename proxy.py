from mitmproxy import ctx
from mitmproxy import http
from mitmproxy.proxy import layer, layers

def load(loader):
    ctx.options.connection_strategy = "lazy"
    ctx.options.upstream_cert = False
    ctx.options.ssl_insecure = True
    ctx.options.allow_hosts = ['enish-games.com']

def request(flow: http.HTTPFlow) -> None:
    if "www-cancer.enish-games.com" in flow.request.pretty_url and \
        "resource/list" not in '/'.join(flow.request.path_components):
        flow.request.host = "127.0.0.1"
        flow.request.port = 443