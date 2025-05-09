// src/api/campaigns.ts

import type { Campaign } from '../types';

export const getCampaigns = (): void => {
  window.chrome.webview?.postMessage({ action: 'getCampaigns' });
};

export const addCampaign = (name: string): void => {
  window.chrome.webview?.postMessage({ action: 'addCampaign', name });
};

export const listenToCampaignUpdates = (handler: (data: any) => void): void => {
  window.chrome.webview?.addEventListener('message', handler);
};

export const removeCampaignListener = (handler: (data: any) => void): void => {
  window.chrome.webview?.removeEventListener('message', handler);
};
