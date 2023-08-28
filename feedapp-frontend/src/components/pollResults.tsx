import { Poll } from '@/common/interfaces/poll'
import { Badge, Card, Center, Flex, Group, Progress, Text } from '@mantine/core'

interface PollResultComponentProps {
  poll: Poll
}

const PollResultComponent = ({ poll }: PollResultComponentProps) => {
  const positivePercentage = Math.round((poll.positiveVotes / poll.countVotes) * 100)
  const negativePercentage = Math.round((poll.negativeVotes / poll.countVotes) * 100)
  return (
    <Center>
      <Card miw={'150px'} w={'500px'} shadow='sm' padding='lg' radius='md' withBorder>
        <Group position='right'>
          <Badge color={poll.isClosed ? 'pink' : 'green'} variant='light' fz={'sm'}>
            {poll.isClosed ? 'Closed' : 'Open'}
          </Badge>
        </Group>
        <Group position='center' mt='md' mb='xs'>
          <Flex direction={'column'}>
            <Text align='center' weight={500} fz={'m'}>
              Poll Question:
            </Text>
            <Text align='center' weight={500} fz={'xl'}>
              {poll.question}
            </Text>
          </Flex>
        </Group>

        <Text size='sm' color='dimmed'>
          {`Poll by: ${poll.creatorName}`}
        </Text>

        <Progress
          mt='md'
          radius='xl'
          size={20}
          sections={[
            { value: positivePercentage, color: 'green', label: `YES (${positivePercentage}%)` },
            { value: negativePercentage, color: 'red', label: `NO (${negativePercentage}%)` },
          ]}
        />
      </Card>
    </Center>
  )
}

export default PollResultComponent
