// src/types.ts

export type Campaign = {
  id: string;
  name: string;
  description?: string;
};

export type Npc = {
  id: string;
  name: string;
  description: string;
  role: string;
  imageUrl?: string;
};

export type Quest = {
  id: string;
  title: string;
  description: string;
  status: 'pending' | 'in_progress' | 'completed' | 'failed';
};

export type MapMarker = {
  id: string;
  x: number;
  y: number;
  label: string;
  type: 'npc' | 'quest' | 'location';
};
