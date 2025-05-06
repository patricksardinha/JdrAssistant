export {};

declare global {
  interface Window {
    chrome: {
      webview: {
        postMessage: (data: unknown) => void;
        addEventListener: (type: string, listener: (event: unknown) => void) => void;
        removeEventListener: (type: string, listener: (event: unknown) => void) => void;
      };
    };
  }
}
