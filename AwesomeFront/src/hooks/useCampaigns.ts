// src/hooks/useCampaigns.ts

import { useEffect, useState } from 'react';
import { getCampaigns, listenToCampaignUpdates, removeCampaignListener } from '../api/campaigns';
import type { Campaign } from '../types';

export const useCampaigns = () => {
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);

  useEffect(() => {
    getCampaigns();

    const handler = (e: MessageEvent) => {
      const data = e.data;
      if (data.type === 'campaignList') {
        setCampaigns(data.campaigns);
      } else if (data.type === 'campaignAdded') {
        setCampaigns((prev) => [...prev, { id: data.id, name: data.name }]);
      }
    };

    listenToCampaignUpdates(handler);

    return () => {
      removeCampaignListener(handler);
    };
  }, []);

  return { campaigns, setCampaigns };
};
