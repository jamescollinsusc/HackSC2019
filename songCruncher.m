%Import table of frequencies for each note
noteFreqs  = csvread('noteFreqs.data');
amp_cutoff = .7;
neighborhood_size = 500;
zero_amp_cutoff = .001;
band_tightness = 3.5;

%Import song
[y,Fs] = audioread('TheAquarium.mp3');

%Shorten song
%yshort = y(1:Fs*2,:);
yshort = y;


%Filter design
nyquist = Fs/2;
bl = zeros(length(noteFreqs),3);
al = zeros(length(noteFreqs),3);
bh = zeros(length(noteFreqs),3);
ah = zeros(length(noteFreqs),3);
%notes = zeros(length(noteFreqs),2,length(y));
notes = zeros(length(y),2,length(noteFreqs));


for i=40:length(noteFreqs)
    if i==1
        lint = noteFreqs(i)-noteFreqs(i)/band_tightness;
        hint = noteFreqs(i) + (noteFreqs(i) - noteFreqs(i+1))/band_tightness;
    elseif i < length(noteFreqs)
        lint = noteFreqs(i) - (noteFreqs(i) - noteFreqs(i-1))/band_tightness;
        hint = noteFreqs(i) + (noteFreqs(i) - noteFreqs(i+1))/band_tightness;
    else
        lint = noteFreqs(i) - (noteFreqs(i) - noteFreqs(i-1))/band_tightness;
        hint = noteFreqs(i)/band_tightness + noteFreqs(i);
    end
    Wnl = lint/nyquist;
    Wnh = hint/nyquist;
    [bl(i,:),al(i,:)] = butter(2,Wnh,'low');
    [bh(i,:),ah(i,:)] = butter(2,Wnl,'high');
    
    
    sfilt1 = filtfilt(bh(i,:),ah(i,:),yshort);
    sfilt2 = filtfilt(bl(i,:),al(i,:),sfilt1);
    hiamp = zeros(length(sfilt2), 2);
    %select high-amplitude portions of the sample
    [hiamp_row, col] = find(abs(sfilt2)>(amp_cutoff*max(max(sfilt2))));
    hiamp_idx = 1;
    hiamp(hiamp_idx:(hiamp_idx+neighborhood_size),:) = sfilt2(hiamp_row-neighborhood_size/2:hiamp_row+neighborhood_size/2,:);
    hiamp_idx = hiamp_idx + neighborhood_size;
    for j=2:length(hiamp_row)
        if (hiamp_row(j) - neighborhood_size/2 >= hiamp_row(j - 1))
            %crawl through until we find a zero-amplitude point-ish
            sample_start_idx = hiamp_row(j)-neighborhood_size/2;
            while ((max(abs(sfilt2(sample_start_idx))) > zero_amp_cutoff) || (sfilt2(sample_start_idx)-sfilt2(sample_start_idx-1) > 0))
                sample_start_idx = sample_start_idx + 1;
%                 max(abs(sfilt2(sample_start_idx)))
            end
            % do the same for the end of the sample
            sample_end_idx = hiamp_row(j)+neighborhood_size/2;
            while ((max(abs(sfilt2(sample_end_idx))) > zero_amp_cutoff) || (sfilt2(sample_end_idx)-sfilt2(sample_end_idx-1) > 0))
                sample_end_idx = sample_end_idx + 1;
%                 max(abs(sfilt2(sample_start_idx)))
            end
            len_sample = (sample_end_idx-sample_start_idx);
            hiamp(hiamp_idx:hiamp_idx+len_sample,:) = sfilt2(sample_start_idx:sample_end_idx,:);
            hiamp_idx = hiamp_idx + len_sample;
        end
    end
    i

    hiamp = hiamp/max(max(hiamp));
    
    notes(:,:,i) =  hiamp;
end

    
        

    

%notes = zeros(length(y),2,length(noteFreqs));
% notes = zeros(length(noteFreqs),length(y),2);


%sound(yshort,Fs)

x=1
while x < 10
    for i=40:90
        sound(notes(1:Fs/4,:,i),Fs)
        pause(1/4)
    end
    x = x+1
end






