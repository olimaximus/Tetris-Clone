using System;

namespace Tetris_Clone
{
    public class BlockQueue
    {
        private readonly Block[] allBlocks = new Block[]
        {
            new IBlock(),
            new JBlock(),
            new LBlock(),
            new OBlock(),
            new SBlock(),
            new TBlock(),
            new ZBlock()
        };

        private readonly Random random = new Random();
        private readonly Queue<Block> queue = new Queue<Block>();

        public BlockQueue()
        {
            FillQueue();
        }

        public Block GetAndUpdate()
        {
            if (queue.Count < 7)
            {
                FillQueue();
            }

            return queue.Dequeue();
        }

        private void FillQueue()
        {
            List<Block> bag = new List<Block>
            {
                allBlocks[0],
                allBlocks[1],
                allBlocks[2],
                allBlocks[3],
                allBlocks[4],
                allBlocks[5],
                allBlocks[6]
            };

            // Fisher-Yates shuffle
            for (int i = bag.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                (bag[i], bag[j]) = (bag[j], bag[i]);
            }

            // Enqueue shuffled blocks
            foreach (var block in bag)
                queue.Enqueue(block);
        }

        public IReadOnlyList<Block> PreviewNextBlocks(int count)
        {
            while (queue.Count < count + 1)
            {
                FillQueue();
            }

            return new List<Block>(queue).GetRange(0, count);
        }

        public Block NextBlock => PreviewNextBlocks(1)[0];
    }
}
