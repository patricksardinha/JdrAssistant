import { useEffect, useState } from 'react';

type Campaign = {
  id: string;
  name: string;
};

export default function CampaignManager() {
  const [name, setName] = useState('');
  const [campaigns, setCampaigns] = useState<Campaign[]>([]);

  useEffect(() => {
    window.chrome.webview?.postMessage({ action: 'getCampaigns' });

    const handler = (e: any) => {
      if (e.data.type === 'campaignList') {
        setCampaigns(e.data.campaigns);
      } else if (e.data.type === 'campaignAdded') {
        setCampaigns((prev) => [...prev, { id: e.data.id, name: e.data.name }]);
      }
    };

    window.chrome.webview?.addEventListener('message', handler);

    return () => {
      window.chrome.webview?.removeEventListener('message', handler);
    };
  }, []);

  const createCampaign = () => {
    if (name.trim()) {
      window.chrome.webview?.postMessage({ action: 'addCampaign', name });
      setName('');
    }
  };

  return (
    <div style={{ padding: 20 }}>
      <h1>📖 Mes campagnes</h1>
      <input
        type="text"
        placeholder="Nom de la campagne"
        value={name}
        onChange={(e) => setName(e.target.value)}
      />
      <button onClick={createCampaign}>Créer</button>

      <ul>
        {campaigns.map((c) => (
          <li key={c.id}>{c.name}</li>
        ))}
      </ul>
    </div>
  );
}
